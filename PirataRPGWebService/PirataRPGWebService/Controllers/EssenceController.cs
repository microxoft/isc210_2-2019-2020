using PirataRPGModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PirataRPGWebService.Controllers
{
    public class EssenceController : ApiController
    {
        ISC210Entities dbContext = new ISC210Entities();
        // GET api/<controller>
        public IEnumerable<EssenceScore> Get()
        {
            return dbContext.EssenceScore
                .OrderByDescending(essence => essence.BlueScore + essence.GreenScore + essence.OrangeScore + essence.PurpleScore + essence.RedScore + essence.YellowScore)
                .Take(2).ToList();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }
        
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public int Put([FromBody]EssenceScore newScore)
        {
            EssenceScore _newEntry = new EssenceScore();
            _newEntry.PlayerName = newScore.PlayerName;
            _newEntry.BlueScore = newScore.BlueScore;
            _newEntry.OrangeScore = newScore.OrangeScore;
            _newEntry.PurpleScore = newScore.PurpleScore;
            _newEntry.RedScore = newScore.RedScore;
            _newEntry.GreenScore = newScore.GreenScore;
            _newEntry.YellowScore = newScore.YellowScore;

            dbContext.EssenceScore.Add(_newEntry);
            return dbContext.SaveChanges();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}