using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZSubmitApp.Core.Mapper.Resolvers
{
    public class HomesteadExemptionWaivedResolver : IMemberValueResolver<object, object, string, string>
    {
        public string Resolve(object source, object destination, string sourceMember, string destMember, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class B1TypeResolver : IMemberValueResolver<object, object, string, string>
    {
        public string Resolve(object source, object destination, string sourceMember, string destMember, ResolutionContext context)
        {
            // TODO: Remove hardcoding
            return sourceMember == "Yes" ? "X" : "";
        }
    }

    public class B2TypeResolver : IMemberValueResolver<object, object, string, string>
    {
        public string Resolve(object source, object destination, string sourceMember, string destMember, ResolutionContext context)
        {
            // TODO: Remove hardcoding
            return sourceMember == "No" ? "X" : "";
        }
    }
    public class B3TypeResolver : IMemberValueResolver<object, object, string, string>
    {
        public string Resolve(object source, object destination, string sourceMember, string destMember, ResolutionContext context)
        {
            // TODO: Remove hardcoding
            return sourceMember == "Cannot be determined" ? "X" : "";
        }
    }
}
