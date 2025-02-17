// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;

namespace Azure.Communication.PhoneNumbers
{
    /// <summary> Model factory for read-only models. </summary>
    public static partial class PhoneNumbersModelFactory
    {
        /// <summary> Initializes new instance of PhoneNumberSearchResult class. </summary>
        /// <param name="searchId"> The search id. </param>
        /// <param name="phoneNumbers"> The phone numbers that are available. Can be fewer than the desired search quantity. </param>
        /// <param name="phoneNumberType"> The phone number&apos;s type, e.g. geographic, or tollFree. </param>
        /// <param name="assignmentType"> Phone number&apos;s assignment type. </param>
        /// <param name="capabilities"> Capabilities of a phone number. </param>
        /// <param name="cost"> The incurred cost for a single phone number. </param>
        /// <param name="searchExpiresOn"> The date that this search result expires and phone numbers are no longer on hold. A search result expires in less than 15min, e.g. 2020-11-19T16:31:49.048Z. </param>
        /// <returns> A new <see cref="PhoneNumbers.PhoneNumberSearchResult"/> instance for mocking. </returns>
        public static PhoneNumberSearchResult PhoneNumberSearchResult(string searchId = default, IReadOnlyList<string> phoneNumbers = default, PhoneNumberType phoneNumberType = default, PhoneNumberAssignmentType assignmentType = default, PhoneNumberCapabilities capabilities = default, PhoneNumberCost cost = default, DateTimeOffset searchExpiresOn = default)
        {
            phoneNumbers ??= new List<string>();
            return new PhoneNumberSearchResult(searchId, phoneNumbers, phoneNumberType, assignmentType, capabilities, cost, searchExpiresOn);
        }

        /// <summary> Initializes new instance of PhoneNumberCost class. </summary>
        /// <param name="amount"> The cost amount. </param>
        /// <param name="isoCurrencySymbol"> The ISO 4217 currency code for the cost amount, e.g. USD. </param>
        /// <param name="billingFrequency"> The frequency with which the cost gets billed. </param>
        /// <returns> A new <see cref="PhoneNumbers.PhoneNumberCost"/> instance for mocking. </returns>
        public static PhoneNumberCost PhoneNumberCost(double amount = default, string isoCurrencySymbol = default, BillingFrequency billingFrequency = default)
        {
            return new PhoneNumberCost(amount, isoCurrencySymbol, billingFrequency);
        }
    }
}
