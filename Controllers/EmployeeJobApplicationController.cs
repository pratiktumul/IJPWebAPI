using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using WebApiAuthenticationToken.Models;
using WebApiAuthenticationToken.Repository;

namespace WebApiAuthenticationToken.Controllers
{
    public class EmployeeJobApplicationController : ApiController
    {
        readonly UserClaimsRepo userClaims;
        private EmployeeJobApplicationController()
        {
            userClaims = new UserClaimsRepo();
        }

        // post method to save the job application in demos table(job application) using HTTP Context class
        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("api/EmployeeJobApplication")]
        public IHttpActionResult PostDemo()
        {
            var userId = userClaims.GetUserClaims((ClaimsIdentity)User.Identity); // calling UserClaims funtion to get the username to get the userid
            using (var db = new IJPDBEntities())
            {
                var httpRequest = HttpContext.Current.Request; // creating an instance of current request of HTTP Context class 
                int JobId = Convert.ToInt32(httpRequest["JobId"]); // JobID in DB is in int format but HTTP request will have it as string: conversion from string to int

                var IsTrue = db.JobApplications.Any(x => x.UserId == userId && x.JobId == JobId);
                if (IsTrue != false)
                {
                    return BadRequest("Sorry, You Have Already Applied For This Job");
                }
                var postedFile = httpRequest.Files["Resume"]; // fetching the file posted by user from HTTP request
                string fileName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName); // Creating unique filename for each file
                var filePath = HttpContext.Current.Server.MapPath("~/Resume/" + fileName);
                postedFile.SaveAs(filePath); // Save the file in specified path

                var YearFromRequest = httpRequest["Year"]; // Year in DB is in int format but HTTP request will have it as string 
                int year = Convert.ToInt32(YearFromRequest); // conversion from string to int
                var MonthFromRequest = httpRequest["Month"]; // Month in DB is in int format but HTTP request will have it as string
                int month = Convert.ToInt32(MonthFromRequest); // conversion from string to int

                JobApplication NewJobApplication = new JobApplication()
                {
                    Ename = httpRequest["Ename"],
                    Curloc = httpRequest["Curloc"],
                    Skill = httpRequest["Skill"],
                    Year = year,
                    Month = month,
                    About = httpRequest["About"],
                    Project = httpRequest["Project"],
                    Resume = filePath,
                    UserId = userId,
                    JobId = Convert.ToInt32(JobId),
                    Status = 1,
                    InterviewDate = null,
                    EmpId = Convert.ToInt32(httpRequest["EmpId"]),
                    ApplyDate = DateTime.Now
                };
                db.JobApplications.Add(NewJobApplication);
                db.SaveChanges();
                return Ok();
            }
        }

        // get method to get user's details for profile component
        [Authorize]
        [HttpGet]
        [Route("api/EmployeeJobApplication")]
        public IHttpActionResult GetUserDetails()
        {
            var userId = userClaims.GetUserClaims((ClaimsIdentity)User.Identity);
            var userDetails = userClaims.FindUserDetails(userId); // Find the user details from users table in DB
            return userDetails != null ? Ok(userDetails) : (IHttpActionResult)NotFound();
        }
    }
}