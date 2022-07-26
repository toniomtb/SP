using System;
using System.Collections;
using System.Linq;
using Microsoft.Extensions.Logging;
using SP.Logic.Cache;
using SP.Logic.Interfaces;
using SP.Model.Models;

namespace SP.Logic
{
    public class PraticheLogic : IPraticheLogic
    {
        private readonly IDataContainer _container;
        private readonly ILogger _logger;

        public PraticheLogic(ILogger logger, IDataContainer container)
        {
            _container = container;
            _logger = logger;
        }

        public int CreateNewPratica(Pratica nuovaPratica)
        {

            try
            {
                nuovaPratica.StatoPratica = StatoPratica.Created;
                nuovaPratica.CronologiaStati = new Queue();
                nuovaPratica.CronologiaStati.Enqueue(nuovaPratica.StatoPratica);
                nuovaPratica.IdPratica = _container.GetAllPratiche().Count != 0
                    ? _container.GetAllPratiche().OrderBy(x => x.IdPratica).Select(x => x.IdPratica).Last() + 1
                    : 1;
                
                _container.AddPratica(nuovaPratica);
                _logger.LogInformation($"Created new pratica with id = {nuovaPratica.IdPratica}");
                return nuovaPratica.IdPratica;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public void UpdatePratica(int idPratica, Pratica updatedPratica)
        {
            try
            {
                var existingPratica = _container.GetAllPratiche().FirstOrDefault(x => x.IdPratica == idPratica);
                if (existingPratica == null)
                {
                    _logger.LogInformation($"Pratica with id {idPratica} does not exists");
                    return;
                }

                existingPratica.Utente = updatedPratica.Utente;
                existingPratica.AllegatoPratica = updatedPratica.AllegatoPratica;

                _logger.LogInformation($"Updated pratica with id = {idPratica}");
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public void ProgressPratica(int idPratica)
        {
            try
            {
                var existingPratica = _container.GetAllPratiche().FirstOrDefault(x => x.IdPratica == idPratica);
                if (existingPratica == null)
                {
                    _logger.LogInformation($"Pratica with id {idPratica} does not exists");
                    return;
                }

                if (existingPratica.StatoPratica != StatoPratica.Completed)
                {
                    existingPratica.StatoPratica = (StatoPratica) Enum.ToObject(typeof(StatoPratica),
                        (int) existingPratica.StatoPratica + 1);
                    existingPratica.CronologiaStati.Enqueue(existingPratica.StatoPratica);

                    _logger.LogInformation($"Pratica with id {idPratica} changed status to: {existingPratica.StatoPratica}");

                    if (existingPratica.StatoPratica == StatoPratica.Completed)
                    {
                        Random rnd = new Random();
                        existingPratica.Approvata = rnd.Next(0, 1) != 0;
                        _logger.LogInformation($"Pratica with id {idPratica} with {existingPratica.StatoPratica} finished with result: {existingPratica.Approvata}");
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public Pratica GetPratica(int idPratica)
        {
            return _container.GetAllPratiche().FirstOrDefault(x => x.IdPratica == idPratica);
        }

        public string GetPdf(int idPratica)
        {
            return _container.GetAllPratiche().FirstOrDefault(x => x.IdPratica == idPratica)?.AllegatoPratica;
        }
    }
}
