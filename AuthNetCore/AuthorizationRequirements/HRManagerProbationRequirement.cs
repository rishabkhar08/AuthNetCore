using Microsoft.AspNetCore.Authorization;

namespace AuthNetCore.AuthorizationRequirements
{
    public class HRManagerProbationRequirement : IAuthorizationRequirement
    {
        public int ProbationMonths {get;}
        public HRManagerProbationRequirement(int probationMonths)
        {
            ProbationMonths = probationMonths;
        }
    }

    public class HRManagerProbationRequirementHandler : AuthorizationHandler<HRManagerProbationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HRManagerProbationRequirement requirement)
        {
            if(!context.User.HasClaim(c => c.Type == "EmploymentDate"))
            {
                return Task.CompletedTask;
            }

            if(DateTime.TryParse(context.User.FindFirst(c => c.Type == "EmploymentDate")?.Value, out DateTime employeementDate))
            {
                var period = DateTime.Now -  employeementDate;
                if(period.Days > 30 * requirement.ProbationMonths) 
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
