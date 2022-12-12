﻿using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    public partial class PixKey
    {
        /// <summary>
        /// PixKey.Log object
        /// <br/>
        /// Every time a PixKey entity is modified, a corresponding PixKey.Log
        /// is generated for the entity. This log is never generated by the
        /// user.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>ID [string]: unique id returned when the log is created. ex: "5656565656565656"</item>
        ///     <item>Key [PixKey]: PixKey entity to which the log refers to.</item>
        ///     <item>Type [string]: type of the PixKey event which triggered the log creation. ex: "created", "registered", "updated", "failed", "canceling", "canceled"</item>
        ///     <item>Errors [list of strings]: list of errors linked to this PixKey event</item>
        ///     <item>Created [DateTime]: creation DateTime for the log. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public class Log : Resource
        {
            public PixKey Key { get; }
            public string Type { get; }
            public List<Dictionary<string, object>> Errors { get; }
            public DateTime Created { get; }

            /// <summary>
            /// PixKey.Log object
            /// <br/>
            /// Every time a PixKey entity is modified, a corresponding PixKey.Log
            /// is generated for the entity. This log is never generated by the
            /// user.
            /// <br/>
            /// Attributes (return-only):
            /// <list>
            ///     <item>id [string]: unique id returned when the log is created. ex: "5656565656565656"</item>
            ///     <item>key [PixKey]: PixKey entity to which the log refers to.</item>
            ///     <item>type [string]: type of the PixKey event which triggered the log creation. ex: "created", "registered", "updated", "failed", "canceling", "canceled"</item>
            ///     <item>errors [list of strings]: list of errors linked to this PixKey event</item>
            ///     <item>created [DateTime]: creation DateTime for the log. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
            /// </list>
            /// </summary>
            public Log(
                string id, PixKey key, string type, List<Dictionary<string, object>> errors, 
                DateTime created 
            ) : base(id)
            {
                Key = key;
                Type = type;
                Errors = errors;
                Created = created;
            }

            /// <summary>
            /// Retrieve a specific PixKey.Log by its id
            /// <br/>
            /// Receive a single PixKey.Log object previously created by the Stark Infra API by its id
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
            ///     <item>PixKey.Log object that corresponds to the given id.</item>
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
            /// Retrieve PixKey.Log objects
            /// <br/>
            /// Receive a IEnumerable of PixKey.Log objects previously created in the Stark Infra API
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
            ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>ids [list of strings, default null]: Log ids to filter PixKey.Log objects. ex: new List<string>{ "5656565656565656" }</item>
            ///     <item>types [list of strings, default null]: filter retrieved objects by types. ex: "created", "registered", "updated", "failed", "canceling", "canceled"</item>
            ///     <item>keyIds [list of strings, default null]: list of PixKey ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.User.Default was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>IEnumerable of PixKey.Log objects with updated attributes</item>
            /// </list>
            /// </summary>
            public static IEnumerable<Log> Query(
                int? limit = null, DateTime? after = null, DateTime? before = null, 
                List<string> ids = null, List<string> types = null, List<string> keyIds = null, 
                User user = null
            ) {
                (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
                return Rest.GetList(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    query: new Dictionary<string, object> {
                        { "limit", limit },
                        { "after", new StarkDate(after) },
                        { "before", new StarkDate(before) },
                        { "ids", ids },
                        { "types", types },
                        { "keyIds", keyIds }
                    },
                    user: user
                ).Cast<Log>();
            }

            /// <summary>
            /// Retrieve paged PixKey.Log objects
            /// <br/>
            /// Receive a list of up to 100 PixKey.Log objects previously created in the Stark Infra API and the cursor to the next page.
            /// Use this function instead of query if you want to manually page your keys.
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
            ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
            ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>ids [list of strings, default null]: Log ids to filter PixKey.Log objects. ex: new List<string>{ "5656565656565656" }</item>
            ///     <item>types [list of strings, default null]: filter retrieved objects by types. ex: "created", "registered", "updated", "failed", "canceling", "canceled"</item>
            ///     <item>keyIds [list of strings, default null]: list of PixKey ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.User.Default was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>list of PixKey.Log objects with updated attributes</item>
            ///     <item>cursor to retrieve the next page of PixKey.Log objects</item>
            /// </list>
            /// </summary>
            public static (List<Log> page, string pageCursor) Page(
                string cursor = null, int? limit = null, DateTime? after = null, DateTime? before = null, 
                List<string> ids = null, List<string> types = null, List<string> keyIds = null, 
                User user = null
            ) {
                (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
                (List<SubResource> page, string pageCursor) = Rest.GetPage(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    query: new Dictionary<string, object> {
                        { "cursor", cursor },
                        { "limit", limit },
                        { "after", new StarkDate(after) },
                        { "before", new StarkDate(before) },
                        { "ids", ids },
                        { "types", types },
                        { "keyIds", keyIds }
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
                return (resourceName: "PixKeyLog", resourceMaker: ResourceMaker);
            }

            internal static Utils.Resource ResourceMaker(dynamic json)
            {
                string id = json.id;
                PixKey key = PixKey.ResourceMaker(json.key);
                string type = json.type;
                List<Dictionary<string, object>> errors = json.errors.ToObject<List<Dictionary<string, object>>>();
                string createdString = json.created;
                DateTime created = Checks.CheckDateTime(createdString);

                return new Log(
                    id: id, key: key, type: type, errors: errors, created: created
                );
            }
        }
    }
}
