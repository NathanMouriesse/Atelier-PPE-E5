using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using ORM_GSB;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Front_GSB.Controllers
{
    public class MedecinsController : Controller
    {
        //private GSB_DATA_Model db = new GSB_DATA_Model();

        // GET: Medecins
        public async Task<ActionResult> Index()
        {
            //L'API a consommer depuis le back
            string url = "https://localhost:44394/api/medecins";

            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("token", "token");
                HttpResponseMessage response = await client.GetAsync(url);

                // si erreur, on propage une exeception
                if (!response.IsSuccessStatusCode)
                    throw new Exception();

                //la liste des médecins
                var med = await response.Content.ReadAsAsync<IEnumerable<Medecin>>();

                return View(med);

            }
        }


        // GET: Medecins/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string url = "https://localhost:44394/api/medecins/" + id;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                //si erreur, on propage une exception
                if (!response.IsSuccessStatusCode)
                    throw new Exception();

                // la liste des médecins
                var med = await response.Content.ReadAsAsync<Medecin>();

                return View(med);
            }
        }
        //    Medecin medecin = await db.Medecins.FindAsync(id);
        //    if (medecin == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(medecin);
        //}

        // GET: Medecins/Create
        [Authorize]
        public async Task<ActionResult> Create()
        {
            string urlDepartements = "https://localhost:44394/api/Departements";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());
                
                HttpResponseMessage response = await client.GetAsync(urlDepartements);

                //si erreur, on propage une exception
                if (!response.IsSuccessStatusCode)
                    throw new Exception();

                //la liste des départements
                var dep = response.Content.ReadAsAsync <IEnumerable<Departement>>().Result.ToList();

                //pour avoir la liste des departements lors de la création d'un medecin
                ViewBag.num_dep = new SelectList(dep, "num_dep", "dep_name");

                return View();

            }
        }
       

        // POST: Medecins/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_medic,Prenom,Nom,Adresse,specialite,Telephone,num_dep")] Medecin medecin)
        {
            if (ModelState.IsValid)
            {

                string json = JsonConvert.SerializeObject(medecin);

                using (HttpClient client = new HttpClient())

                
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());
                    //client.DefaultRequestHeaders.Add("token", "123456789");
                    using (var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44394/api/medecins"))
                    {
                        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                        //envoie des infos
                        var send = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                        if (!send.IsSuccessStatusCode)
                            throw new Exception("Un problème s'est produit, veuillez contacter l'administrateur");

                        send.EnsureSuccessStatusCode();
                        //ViewBag.num_dep = new SelectList(db.Departements, "num_dep", "dep_name", medecin.num_dep);
                        return RedirectToAction("Index");
                    }

                }
                
                //db.Medecins.Add(medecin);
                //await db.SaveChangesAsync();
                //return RedirectToAction("Index");
            }

            //ViewBag.num_dep = new SelectList(db.Departements, "num_dep", "dep_name", medecin.num_dep);
            return View(medecin);
        }

        // GET: Medecins/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string url = "https://localhost:44394/api/medecins/" + id;

            using (HttpClient client = new HttpClient())

            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());
                
                HttpResponseMessage response = await client.GetAsync(url);

                //si erreur, on propage une exception
                if (!response.IsSuccessStatusCode)
                    throw new Exception();

                // la liste des médecins
                var med = await response.Content.ReadAsAsync<Medecin>();

                //ViewBag.num_dep = new SelectList(db.Departements, "num_dep", "dep_name");
                return View(med);
            }
            }

        // POST: Medecins/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_medic,Prenom,Nom,Adresse,specialite,Telephone,num_dep")] Medecin medecin)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(medecin);

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());
                    //client.DefaultRequestHeaders.Add("token", "123456789");
                    HttpContent cont = new StringContent(json, Encoding.UTF8, "application/json");

                    var send = await client.PutAsync("https://localhost:44394/api/medecins/" + medecin.id_medic, cont).ConfigureAwait(false);
                    if (!send.IsSuccessStatusCode)
                        throw new Exception();

                    send.EnsureSuccessStatusCode();
                    //ViewBag.num_dep = new SelectList(db.Departements, "num_dep", "dep_name", medecin.num_dep);
                    return RedirectToAction("Index");
                }
                
            }
            
            return View(medecin);
        }

        // GET: Medecins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? id)
        {
            string url = "https://localhost:44394/api/medecins/" + id;

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());
                //client.DefaultRequestHeaders.Add("token", " 123456789");

                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    throw new Exception();
                var medecin = await response.Content.ReadAsAsync<Medecin>();

                return View(medecin);

            }

        }

        // POST: Medecins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            string url = "https://localhost:44394/api/medecins/" + id;

            if (id == null) ;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());
                //client.DefaultRequestHeaders.Add("token", " 123456789");

                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    throw new Exception();
            }

            return RedirectToAction("Index");

            }

        //lire le token
        private string ReadToken()
        {
            string token = string.Empty;
            try
            {
                string fileName = @"C:/temp/token.txt";
                token = System.IO.File.ReadAllText(fileName);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return token;
                }
    }
    }

