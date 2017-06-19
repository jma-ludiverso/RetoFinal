using Mario.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Services
{
    public class MarioService
    {

        private static MarioService _instance;
        private MobileServiceClient _mobileService;
        private IMobileServiceTable<Practica> _tbPracticas;
        private IMobileServiceTable<PracticasDetalle> _tbPractica_Detalles;

        public static MarioService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MarioService();
                }

                return _instance;
            }
        }

        public MobileServiceClient Client
        {
            get { return _mobileService; }
        }

        public MarioService()
        {
            _mobileService = new MobileServiceClient(GlobalSettings.AzureEndpoint);
            _tbPracticas = _mobileService.GetTable<Practica>();
            _tbPractica_Detalles = _mobileService.GetTable<PracticasDetalle>();
        }

        public async Task AddPractice(Practica item)
        {
            try
            {
                await _tbPracticas.InsertAsync(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Error {0}", ex.Message);
            }
        }

        public async Task AddPracticeExercise(PracticasDetalle exercise)
        {
            try
            {
                await _tbPractica_Detalles.InsertAsync(exercise);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Error {0}", ex.Message);
            }
        }

        public async Task DeletePractice(Practica item)
        {
            try
            {
                await _tbPracticas.DeleteAsync(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Error {0}", ex.Message);
            }
        }

        public async Task EditPractice(Practica item)
        {
            try
            {
                await _tbPracticas.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Error {0}", ex.Message);
            }
        }

        public async Task<List<Practica>> GetItems()
        {
            List<Practica> ret = null;
            try
            {
                IEnumerable<Practica> Items = await _tbPracticas.ReadAsync();
                ret = new List<Practica>(Items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Error {0}", ex.Message);
                ret = new List<Practica>();
            }
            return ret;
        }

        public async Task<List<PracticasDetalle>> GetDetailItems(string IdPractica)
        {
            List<PracticasDetalle> ret = null;
            try
            {
                var query = _tbPractica_Detalles.CreateQuery().Where(p => p.IdPractica == IdPractica);
                IEnumerable<PracticasDetalle> Items = await _tbPractica_Detalles.ReadAsync(query);
                ret = new List<PracticasDetalle>(Items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Error {0}", ex.Message);
                ret = new List<PracticasDetalle>();
            }
            return ret;
        }

    }
}
