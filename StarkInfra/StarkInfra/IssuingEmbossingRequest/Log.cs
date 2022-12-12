using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    public partial class IssuingEmbossingRequest
    {
        /// <summary>
        /// IssuingEmbossingRequest.Log object
        /// <br/>
        /// Every time an IssuingEmbossingRequest entity is modified, a corresponding IssuingEmbossingRequest.Log
        /// is generated for the entity. This log is never generated by the
        /// user, but it can be retrieved to check additional information
        /// on the IssuingEmbossingRequest.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>ID [string]: unique id returned when the log is created. ex: "5656565656565656"</item>
        ///     <item>Request [IssuingEmbossingRequest]: IssuingEmbossingRequest entity to which the log refers to.</item>
        ///     <item>Type [string]: type of the IssuingEmbossingRequest event which triggered the log creation. ex: "created", "sending", "sent", "processing", "success", "failed"</item>
        ///     <item>Errors [list of dictionaries]: list of errors linked to this IssuingEmbossingRequest event</item>
        ///     <item>Created [DateTime]: creation datetime for the log. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public class Log : Resource
        {
            public DateTime Created { get; }
            public string Type { get; }
            public List<Dictionary<string, object>> Errors { get; }
            public IssuingEmbossingRequest Card { get; }

            /// <summary>
            /// IssuingEmbossingRequest.Log object
            /// <br/>
            /// Every time an IssuingEmbossingRequest entity is modified, a corresponding IssuingEmbossingRequest.Log
            /// is generated for the entity. This log is never generated by the
            /// user, but it can be retrieved to check additional information
            /// on the IssuingEmbossingRequest.
            /// <br/>
            /// Attributes (return-only):
            /// <list>
            ///     <item>id [string]: unique id returned when the log is created. ex: "5656565656565656"</item>
            ///     <item>request [IssuingEmbossingRequest]: IssuingEmbossingRequest entity to which the log refers to.</item>
            ///     <item>type [string]: type of the IssuingEmbossingRequest event which triggered the log creation. ex: "created", "sending", "sent", "processing", "success", "failed"</item>
            ///     <item>errors [list of dictionaries]: list of errors linked to this IssuingEmbossingRequest event</item>
            ///     <item>created [DateTime]: creation datetime for the log. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
            /// </list>
            /// </summary>
            public Log(string id, DateTime created, string type, List<Dictionary<string, object>> errors, IssuingEmbossingRequest request) : base(id)
            {
                Created = created;
                Type = type;
                Errors = errors;
                Card = request;
            }

            /// <summary>
            /// Retrieve a specific IssuingEmbossingRequest.Log
            /// <br/>
            /// Receive a single IssuingEmbossingRequest.Log object previously created by the Stark Infra API by passing its id
            /// <br/>
            /// Parameters (required):
            /// <list>
            ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
            /// </list>
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
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
            /// Retrieve IssuingEmbossingRequest.Log objects
            /// <br/>
            /// Receive an IEnumerable of IssuingEmbossingRequest.Log objects previously created in the Stark Infra API
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
            ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>types [list of strings, default null]: filter retrieved objects by types. ex: new List<string>{ "created", "sending", "sent", "processing", "success", "failed" }</item>
            ///     <item>requestIds [list of strings, default null]: list of IssuingEmbossingRequest ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>IEnumerable of IssuingEmbossingRequest.Log objects with updated attributes</item>
            /// </list>
            /// </summary>
            public static IEnumerable<Log> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
                List<string> types = null, List<string> requestIds = null, User user = null)
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
                        { "requestIds", requestIds }
                    },
                    user: user
                ).Cast<Log>();
            }

            /// <summary>
            /// Retrieve paged IssuingEmbossingRequest.Log objects
            /// <br/>
            /// Receive a list of up to 100 IssuingEmbossingRequest.Log objects previously created in the Stark Infra API and the cursor to the next page.
            /// Use this function instead of query if you want to manually page your requests.
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
            ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
            ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects.ex: new List<string>{  "5656565656565656", "4545454545454545" }</item>
            ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>types [list of strings, default null]: filter retrieved objects by types. ex: new List<string>{  "created", "sending", "sent", "processing", "success", "failed" }</item>
            ///     <item>requestIds [list of strings, default null]: list of IssuingEmbossingRequest ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>list of IssuingEmbossingRequest.Log objects with updated attributes</item>
            ///     <item>cursor to retrieve the next page of IssuingEmbossingRequest.Log objects</item>
            /// </list>
            /// </summary>
            public static (List<Log> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
                DateTime? before = null, List<string> types = null, List<string> requestIds = null, User user = null)
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
                        { "requestIds", requestIds }
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
                return (resourceName: "IssuingEmbossingRequestLog", resourceMaker: ResourceMaker);
            }

            internal static Utils.Resource ResourceMaker(dynamic json)
            {
                List<Dictionary<string, object>> errors = json.errors.ToObject<List<Dictionary<string, object>>>();
                string id = json.id;
                string createdString = json.created;
                DateTime created = Checks.CheckDateTime(createdString);
                string type = json.type;
                IssuingEmbossingRequest request = IssuingEmbossingRequest.ResourceMaker(json.request);

                return new Log(id: id, created: created, type: type, errors: errors, request: request);
            }
        }
    }
}
