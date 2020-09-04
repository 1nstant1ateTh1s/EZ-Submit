using EZSubmitApp.Core.Constants;
using EZSubmitApp.Core.DTOs;
using EZSubmitApp.Core.Entities;
using System;
using System.Collections.Generic;

namespace EZSubmitApp.Core.JsonConverters
{
    /// <summary>
    /// Polymorphic Json converter for CaseForm entity classes.
    /// </summary>
    public class CaseFormJsonConverter : DerivedTypeJsonConverter<CaseForm>
    {
        protected override string TypePropertyName { get; } = CaseFormConstants.TYPE_JSON_PROPERTY_NAME;
        protected override Dictionary<string, Type> TypeMap { get; } = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
                        {
                            { CaseFormTypes.WARRANT_IN_DEBT, typeof(WarrantInDebtForm) },
                            { CaseFormTypes.SUMMONS_FOR_UNLAWFUL_DETAINER, typeof(SummonsForUnlawfulDetainerForm) }
                        };
    }

    /// <summary>
    /// Polymorphic Json converter for CaseForm DTO classes.
    /// </summary>
    public class CaseFormDtoJsonConverter : DerivedTypeJsonConverter<CaseFormDto>
    {
        protected override string TypePropertyName => CaseFormConstants.TYPE_JSON_PROPERTY_NAME;

        protected override Dictionary<string, Type> TypeMap => new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
                        {
                            { CaseFormTypes.WARRANT_IN_DEBT, typeof(WarrantInDebtFormDto) },
                            { CaseFormTypes.SUMMONS_FOR_UNLAWFUL_DETAINER, typeof(SummonsForUnlawfulDetainerFormDto) }
                        };
    }

    /// <summary>
    /// Polymorphic Json converter for CaseForm Creation DTO classes.
    /// </summary>
    public class CaseFormForCreationDtoJsonConverter : DerivedTypeJsonConverter<CaseFormForCreationDto>
    {
        protected override string TypePropertyName => CaseFormConstants.TYPE_JSON_PROPERTY_NAME;

        protected override Dictionary<string, Type> TypeMap => new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
                        {
                            { CaseFormTypes.WARRANT_IN_DEBT, typeof(WarrantInDebtFormForCreationDto) },
                            { CaseFormTypes.SUMMONS_FOR_UNLAWFUL_DETAINER, typeof(SummonsForUnlawfulDetainerFormForCreationDto) }
                        };
    }

    /// <summary>
    /// Polymorphic Json converter for CaseForm Update DTO classes.
    /// </summary>
    public class CaseFormForUpdateDtoJsonConverter : DerivedTypeJsonConverter<CaseFormForUpdateDto>
    {
        protected override string TypePropertyName => CaseFormConstants.TYPE_JSON_PROPERTY_NAME;

        protected override Dictionary<string, Type> TypeMap => new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
                        {
                            { CaseFormTypes.WARRANT_IN_DEBT, typeof(WarrantInDebtFormForUpdateDto) },
                            { CaseFormTypes.SUMMONS_FOR_UNLAWFUL_DETAINER, typeof(SummonsForUnlawfulDetainerFormForUpdateDto) }
                        };
    }
}
