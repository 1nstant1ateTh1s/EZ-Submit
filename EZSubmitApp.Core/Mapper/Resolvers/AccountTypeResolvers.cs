using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZSubmitApp.Core.Mapper.Resolvers
{
    public class AccountTypeResolver : IMemberValueResolver<object, object, string, string>
    {
        public string Resolve(object source, object destination, string sourceMember, string destMember, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class A1TypeResolver : IMemberValueResolver<object, object, string, string>
    {
        public string Resolve(object source, object destination, string sourceMember, string destMember, ResolutionContext context)
        {
            return sourceMember == "Open Account" ? "X" : "";
        }
    }

    public class A2TypeResolver : IMemberValueResolver<object, object, string, string>
    {
        public string Resolve(object source, object destination, string sourceMember, string destMember, ResolutionContext context)
        {
            return sourceMember == "Contract" ? "X" : "";
        }
    }
    public class A3TypeResolver : IMemberValueResolver<object, object, string, string>
    {
        public string Resolve(object source, object destination, string sourceMember, string destMember, ResolutionContext context)
        {
            return sourceMember == "Note" ? "X" : "";
        }
    }
    public class A4TypeResolver : IMemberValueResolver<object, object, string, string>
    {
        public string Resolve(object source, object destination, string sourceMember, string destMember, ResolutionContext context)
        {
            return sourceMember == "Other" ? "X" : "";
        }
    }
}
