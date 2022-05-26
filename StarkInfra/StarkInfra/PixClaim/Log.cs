﻿using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    public partial class PixClaim
    {
        /// <summary>
        /// PixClaim.Log object
        /// <br/>
        /// Every time a PixClaim entity is modified, a corresponding PixClaim.Log
        /// is generated for the entity. This log is never generated by the
        /// user.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>ID [string]: unique id returned when the log is created. ex: "5656565656565656"</item>
        ///     <item>Created [DateTime]: creation datetime for the log. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>Type [string]: type of the PixClaim event which triggered the log creation. ex: "created", "failed", "delivering", "delivered", "confirming", "confirmed", "success", "canceling" and "canceled"</item>
        ///     <item>Errors [list of strings]: list of errors linked to this PixClaim event</item>
        ///     <item>Agent [string]: agent that modified the PixClaim resulting in the Log. Options: "claimer", "claimed".</item>
        ///     <item>Reason [string]: reason why the PixClaim was modified, resulting in the Log. Options: "fraud", "userRequested", "accountClosure", "defaultOperation", "reconciliation".</item>
        ///     <item>Claim [PixClaim]: PixClaim entity to which the log refers to.</item>
        /// </list>
        /// </summary>
        public class Log : Resource
        {
            public DateTime Created { get; }
            public string Type { get; }
            public List<Dictionary<string, object>> Errors { get; }
            public string Agent { get;  }
            public string Reason { get;  }
            public PixClaim Claim { get; }

            /// <summary>
            /// PixClaim.Log object
            /// <br/>
            /// Every time a PixClaim entity is modified, a corresponding PixClaim.Log
            /// is generated for the entity. This log is never generated by the
            /// user.
            /// <br/>
            /// Attributes (return-only):
            /// <list>
            ///     <item>id [string]: unique id returned when the log is created. ex: "5656565656565656"</item>
            ///     <item>created [DateTime]: creation datetime for the log. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
            ///     <item>type [string]: type of the PixClaim event which triggered the log creation. ex: "created", "failed", "delivering", "delivered", "confirming", "confirmed", "success", "canceling" and "canceled"</item>
            ///     <item>errors [list of strings]: list of errors linked to this PixClaim event</item>
            ///     <item>agent [string]: agent that modified the PixClaim resulting in the Log. Options: "claimer", "claimed".</item>
            ///     <item>reason [string]: reason why the PixClaim was modified, resulting in the Log. Options: "fraud", "userRequested", "accountClosure", "defaultOperation", "reconciliation".</item>
            ///     <item>claim [PixClaim]: PixClaim entity to which the log refers to.</item>
            /// </list>
            /// </summary>
            public Log(string id, DateTime created, string type, List<Dictionary<string, object>> errors, string agent, string reason, PixClaim claim) : base(id)
            {
                Created = created;
                Type = type;
                Errors = errors;
                Claim = claim;
                Agent = agent;
                Reason = reason;
            }

            /// <summary>
            /// Retrieve a specific Log
            /// <br/>
            /// Receive a single Log object previously created by the Stark Infra API by passing its id
            /// <br/>
            /// Parameters (required):
            /// <list>
            ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
            /// </list>
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>Log object with updated attributes</item>
            /// </list>
            /// </summary>
            public static Log Get(string id, User user = null)
            {
                (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
                return Rest.GetId(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    id: id,
                    user: user
                ) as Log;
            }

            /// <summary>
            /// Retrieve Logs
            /// <br/>
            /// Receive an IEnumerable of Log objects previously created in the Stark Infra API
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>ids [list of strings, default null]: Log ids to filter PixClaim Logs. ex: new List<string>{ "5656565656565656" }</item>
            ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
            ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>types [list of strings, default null]: filter retrieved objects by types. ex: new List<string>{ "created", "failed", "delivering", "delivered", "confirming", "confirmed", "success", "canceling" and "canceled" }</item>
            ///     <item>claimIds [list of strings, default null]: list of PixClaim ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.User.Default was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>IEnumerable of Log objects with updated attributes</item>
            /// </list>
            /// </summary>
            public static IEnumerable<Log> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
                List<string> types = null, List<string> ids = null, List<string> claimIds = null, User user = null)
            {
                (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
                return Rest.GetList(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    query: new Dictionary<string, object> {
                        { "limit", limit },
                        { "after", new StarkDate(after) },
                        { "before", new StarkDate(before) },
                        { "types", types },
                        { "claimIds", claimIds },
                        { "ids", ids }
                    },
                    user: user
                ).Cast<Log>();
            }

            /// <summary>
            /// Retrieve paged Logs
            /// <br/>
            /// Receive a list of up to 100 Log objects previously created in the Stark Infra API and the cursor to the next page.
            /// Use this function instead of query if you want to manually page your claims.
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
            ///     <item>ids [list of strings, default null]: Log ids to filter PixClaim Logs. ex: new List<string>{ "5656565656565656" }</item>
            ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
            ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>types [list of strings, default null]: filter retrieved objects by types. ex: new List<string>{ "created", "failed", "delivering", "delivered", "confirming", "confirmed", "success", "canceling" and "canceled" }</item>
            ///     <item>claimIds [list of strings, default null]: list of PixClaim ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.User.Default was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>list of Log objects with updated attributes</item>
            ///     <item>cursor to retrieve the next page of Log objects</item>
            /// </list>
            /// </summary>
            public static (List<Log> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
                DateTime? before = null, List<string> types = null, List<string> ids = null, List<string> claimIds = null, User user = null)
            {
                (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
                (List<SubResource> page, string pageCursor) = Rest.GetPage(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    query: new Dictionary<string, object> {
                        { "cursor", cursor },
                        { "limit", limit },
                        { "after", new StarkDate(after) },
                        { "before", new StarkDate(before) },
                        { "types", types },
                        { "claimIds", claimIds },
                        { "ids", ids }
                    },
                    user: user
                );
                List<Log> logs = new List<Log>();
                foreach (SubResource subResource in page)
                {
                    logs.Add(subResource as Log);
                }
                return (logs, pageCursor);
            }

            internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
            {
                return (resourceName: "PixClaimLog", resourceMaker: ResourceMaker);
            }

            internal static Utils.Resource ResourceMaker(dynamic json)
            {
                List<Dictionary<string, object>> errors = json.errors.ToObject<List<Dictionary<string, object>>>();
                string id = json.id;
                string createdString = json.created;
                DateTime created = Checks.CheckDateTime(createdString);
                string type = json.type;
                string agent = json.agent;
                string reason = json.reason;
                PixClaim claim = PixClaim.ResourceMaker(json.claim);

                return new Log(id: id, created: created, type: type, errors: errors, claim: claim, agent: agent, reason: reason);
            }
        }
    }
}
