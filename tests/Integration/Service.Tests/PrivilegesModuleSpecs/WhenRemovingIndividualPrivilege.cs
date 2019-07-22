//namespace Linn.Authorisation.Service.Tests.PermissionsModuleSpecs
//{
//    using FluentAssertions;
//    using Linn.Authorisation.Domain;
//    using Linn.Authorisation.Domain.Permissions;
//    using Linn.Authorisation.Resources;
//    using Linn.Common.Facade;
//    using Nancy;
//    using Nancy.Testing;
//    using NSubstitute;
//    using NUnit.Framework;

//    public class WhenRemovingIndividualPrivilege : ContextBase
//    {
//        [SetUp]
//        public void SetUp()
//        {
//            var requestResource = new PrivilegeResource { Name = "New privilege", Active = false };

//            var p = new IndividualPermission("/employee/19", new Privilege("Name"), "/employee/33087");

//            this.PermissionService.RemovePermission(Arg.Any<PermissionResource>())
//                .Returns(new SuccessResult<Permission>(p));

//            this.Response = this.Browser.Delete(
//                "/authorisation/privilege/19",
//                with =>
//                    {
//                        with.Header("Accept", "application/json");
//                        with.Header("Content-Type", "application/json");
//                        with.Query("granteeUri", "/privilege/19");
//                    }).Result;
//        }

//        [Test]
//        public void ShouldReturnOk()
//        {
//            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
//        }

//        [Test]
//        public void ShouldCallService()
//        {
//            this.PermissionService.RemovePermission(Arg.Any<PermissionResource>()).ReceivedCalls();
//        }

//        [Test]
//        public void ShouldReturnResource()
//        {
//            var resource = this.Response.Body.DeserializeJson<PermissionResource>();
//            resource.GranteeUri.Should().Be("/privilege/19");
//        }
//    }
//}