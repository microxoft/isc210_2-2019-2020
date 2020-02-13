using PirataRPGModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PirataRPGWebService.Controllers
{
    public class ValuesController : ApiController
    {
        ISC210Entities dbContext = new ISC210Entities();

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public int Post([FromBody]GravilotaScore newScore)
        {
            GravilotaScore gScore = new GravilotaScore();
            gScore.Id = 1;
            gScore.PlayerName = newScore.PlayerName;
            gScore.Score = newScore.Score;

            dbContext.GravilotaScore.Add(gScore);
            return dbContext.SaveChanges();
        }

        // PUT api/values/5
        public int Put([FromBody]GravilotaScore newScore)
        {
            GravilotaScore gScore = new GravilotaScore();
            gScore.Id = 6;
            gScore.PlayerName = newScore.PlayerName;
            gScore.Score = newScore.Score;

            dbContext.GravilotaScore.Add(gScore);
            return dbContext.SaveChanges();
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
