using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static SafeAces.App;
using Firebase.Database.Query;
using static SafeAces.Includes.GlobalVariables;

namespace SafeAces.Models
{
    public class Visitor
    {
        public Guid Id { get; set; }
        public string BarcodeId { get; set; }
        public string Purpose { get; set; }
        public string FullName { get; set; }
        public string Date { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }

        public async Task<bool> _AddVisitor(string barcodeId, string purpose, string fullname, string date, string timein, string timeout)
        {
            var visitor = new Visitor()
            {
                Id = Guid.NewGuid(),
                BarcodeId = barcodeId,
                Purpose = purpose,
                FullName = fullname,
                Date = date,
                TimeIn = timein,
                TimeOut = timeout
            };
            await client.Child("Visitor").PostAsync(visitor);
            return true;
        }

        public async Task<Visitor> GetVisitor(string barcodeId)
        {
            var visitors = await client
               .Child("Visitor")
               .OnceAsync<Visitor>();

            return visitors
               .Where(item => item.Object.BarcodeId == barcodeId)
               .Select(item => new Visitor
               {
                   Id = item.Object.Id,
                   BarcodeId = item.Object.BarcodeId,
                   Purpose = item.Object.Purpose,
                   FullName = item.Object.FullName,
                   Date = item.Object.Date,
                   TimeIn = item.Object.TimeIn,
                   TimeOut = item.Object.TimeOut
               })
               .FirstOrDefault();
        }

        public async Task<bool> GetVisID(string id)
        {
            try
            {
                var evaluateID =
                    (await client.Child("Visitor").OnceAsync<Visitor>()).FirstOrDefault(a =>
                    a.Object.BarcodeId == id);
                if (evaluateID != null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetVisitorKey(Guid id)
        {
            var evaluateID = (await client
               .Child("Visitor")
               .OnceAsync<Visitor>()).FirstOrDefault
                (a => a.Object.Id == id);
            if (evaluateID != null)
            {
                visitorkey = evaluateID.Key;
                id = evaluateID.Object.Id;
                purpose = evaluateID.Object.Purpose;
                date = evaluateID.Object.Date;
                fullname = evaluateID.Object.FullName;
                timein = evaluateID.Object.TimeIn;

                return evaluateID.Key;
            }
            return null;
        }

        public async Task<bool> EditVisitor(Guid id, string purpose, string fullname, string date, string timein, string timeout)
        {
            try
            {
                var visitorKey = await GetVisitorKey(id);
                if (visitorKey != null)
                {
                    var visitor = new Visitor()
                    {
                        Id = id,
                        Purpose = purpose,
                        FullName = fullname,
                        Date = date,
                        TimeIn = timein,
                        TimeOut = timeout,
                    };

                    await client.Child($"Visitor/{visitorkey}").PatchAsync(visitor);
                    client.Dispose();
                    return true;
                }
                else
                {
                    return false; // Return false if visitorKey is null
                }
            }
            catch (Exception ex)
            {
                client.Dispose();
                return false;
            }
        }

        public async Task<bool> DeleteVisitor(Guid id)
        {
            try
            {
                var getvisitorkey = (await client
                   .Child("Visitor")
                   .OnceAsync<Visitor>()).FirstOrDefault
                    (a => a.Object.Id == id);
                if (getvisitorkey != null)
                {
                    await client
                       .Child("Visitor")
                   .Child(getvisitorkey.Key)
                       .DeleteAsync();
                    client.Dispose();
                    return true;
                }

                client.Dispose();
                return false;
            }
            catch
            {
                client.Dispose();
                return false;
            }
        }
    }
}
