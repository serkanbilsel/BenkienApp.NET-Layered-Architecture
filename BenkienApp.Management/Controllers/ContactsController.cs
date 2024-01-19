using BenkienApp.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BenkienApp.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContactsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiContacts = "https://localhost:7022/api/Contacts";

        public ContactsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }




        public async Task<ActionResult> Index()
        {
            // ViewBag içinde SelectList kullanımına devam edebilirsiniz, ancak modeli Contact olarak değiştirmeniz gerekiyor.
            ViewBag.Contacts = new SelectList(await _httpClient.GetFromJsonAsync<List<Contact>>(_apiContacts));

            // Dönen modeli Contact olarak değiştirin.
            var model = await _httpClient.GetFromJsonAsync<List<Contact>>(_apiContacts);
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _httpClient.GetFromJsonAsync<Contact>(_apiContacts + "/" + id);

            if (product == null)
            {
                return NotFound();
            }
            return View("Index");
        }
        public async Task<ActionResult> Create()
        {
            // Create işlemi için boş bir Contact modeli oluşturun.
            var newContact = new Contact();
            return View(newContact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Contact newContact)
        {
            try
            {
                // Yeni Contact'ı API'ye göndererek oluşturun.
                await _httpClient.PostAsJsonAsync(_apiContacts, newContact);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Hata durumunda tekrar Create sayfasını gösterin.
                return View(newContact);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            // Edit işlemi için mevcut Contact'ı API'den alın.
            var existingContact = await _httpClient.GetFromJsonAsync<Contact>(_apiContacts + "/" + id);

            if (existingContact == null)
            {
                return NotFound();
            }

            return View(existingContact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Contact updatedContact)
        {
            try
            {
                // Güncellenmiş Contact'ı API'ye göndererek güncelleyin.
                await _httpClient.PutAsJsonAsync(_apiContacts + "/" + id, updatedContact);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Hata durumunda tekrar Edit sayfasını gösterin.
                return View(updatedContact);
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            // Delete işlemi için mevcut Contact'ı API'den alın.
            var contactToDelete = await _httpClient.GetFromJsonAsync<Contact>(_apiContacts + "/" + id);

            if (contactToDelete == null)
            {
                return NotFound();
            }

            return View(contactToDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Contact collection)
        {
            // Contact'ı API'den silin.
            await _httpClient.DeleteAsync(_apiContacts + "/" + id);
            return RedirectToAction(nameof(Index));
        }


    }
}
