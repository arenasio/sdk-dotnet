﻿using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixBalance object
    /// <br/>
    /// The PixBalance object displays the current balance of the workspace,
    /// which is the result of the sum of all transactions within this
    /// workspace. The balance is never generated by the user, but it
    /// can be retrieved to see the available information.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: unique id returned when PixBalance is created. ex: "5656565656565656"</item>
    ///     <item>Amount [long]: current balance amount of the workspace in cents. ex: 200 (= R$ 2.00)</item>
    ///     <item>Currency [string]: currency of the current workspace. Expect others to be added eventually. ex: "BRL"</item>
    ///     <item>Updated [DateTime]: latest update datetime for the balance. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public class PixBalance : Resource
    {
        public long Amount { get; }
        public string Currency { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// PixBalance object
        /// <br/>
        /// The PixBalance object displays the current balance of the workspace,
        /// which is the result of the sum of all transactions within this
        /// workspace.The PixBalance is never generated by the user, but it
        /// can be retrieved to see the available information.
        /// <br/>
        /// Attributes(return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the PixBalance is created. ex: "5656565656565656"</item>
        ///     <item>amount [long]: current balance amount of the workspace in cents. ex: 200 (= R$ 2.00)</item>
        ///     <item>currency [string]: currency of the current workspace. Expect others to be added eventually. ex: "BRL"</item>
        ///     <item>updated [DateTime]: latest update datetime for the balance. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PixBalance(string id, long amount, string currency, DateTime updated) : base(id)
        {
            Amount = amount;
            Currency = currency;
            Updated = updated;
        }

        /// <summary>
        /// Retrieve the PixBalance object
        /// <br/>
        /// Receive the PixBalance object linked to your workspace in the Stark Infra API
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>PixBalance object with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixBalance Get(User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object>(),
                user: user
            ).First() as PixBalance;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixBalance", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            long amount = json.amount;
            string currency = json.currency;
            string updatedString = json.updated;
            DateTime updated = Checks.CheckDateTime(updatedString);

            return new PixBalance(id: id, amount: amount, currency: currency, updated: updated);
        }
    }
}
