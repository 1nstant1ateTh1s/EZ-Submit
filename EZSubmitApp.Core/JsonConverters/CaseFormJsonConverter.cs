using EZSubmitApp.Core.Constants;
using EZSubmitApp.Core.Entities;
using System;
using System.Collections.Generic;

namespace EZSubmitApp.Core.JsonConverters
{
    public class CaseFormJsonConverter : DerivedTypeJsonConverter<CaseForm>
    {
        protected override string TypePropertyName { get; } = CaseFormConstants.TYPE_JSON_PROPERTY_NAME;
        protected override Dictionary<string, Type> TypeMap { get; } = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
                        {
                            { CaseFormTypes.WARRANT_IN_DEBT, typeof(WarrantInDebtForm) },
                            { CaseFormTypes.SUMMONS_FOR_UNLAWFUL_DETAINER, typeof(SummonsForUnlawfulDetainerForm) }
                        };
    }
}
