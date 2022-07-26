using System;
using Microsoft.Extensions.Logging;
using SP.Logic;
using SP.Logic.Cache;
using SP.Logic.Interfaces;
using SP.Model.Models;
using Xunit;

namespace SP.Test
{
    public class PraticheLogicTest
    {
        private static ILogger logger;
        DataContainer _dataContainer;
        IPraticheLogic _logic;

        public PraticheLogicTest()
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            logger = loggerFactory.CreateLogger<Program>();

            _dataContainer = new DataContainer();
            _logic = new PraticheLogic(logger, _dataContainer);
        }

        [Fact]
        public void CreateNewPraticaTest()
        {
            Assert.Equal(0, _dataContainer.GetAllPratiche()?.Count);

            Pratica nuovaPratica = new Pratica();
            nuovaPratica.AllegatoPratica = "test";
            nuovaPratica.Utente = new Utente
            {
                CodiceFiscale = "CFTEST12345",
                Cognome = "Cognome",
                Nome = "Nome",
                DataDiNascita = DateTime.Now,
            };

            _logic.CreateNewPratica(nuovaPratica);

            Assert.Equal(1, _dataContainer.GetAllPratiche()?.Count);
        }

        [Fact]
        public void UpdatePraticaTest()
        {
            Assert.Equal(0, _dataContainer.GetAllPratiche()?.Count);

            Pratica nuovaPratica = new Pratica();
            nuovaPratica.AllegatoPratica = "test";
            nuovaPratica.Utente = new Utente
            {
                CodiceFiscale = "CFTEST12345",
                Cognome = "Cognome",
                Nome = "Nome",
                DataDiNascita = DateTime.Now,
            };

            _logic.CreateNewPratica(nuovaPratica);

            Assert.Equal(1, _dataContainer.GetAllPratiche()?.Count);

            Pratica receivedPratica = _logic.GetPratica(1);
            Assert.Equal(1, receivedPratica.IdPratica);

            nuovaPratica.AllegatoPratica = "test2";
            nuovaPratica.Utente.Nome = "Antonio";

            _logic.UpdatePratica(1, nuovaPratica);

            receivedPratica = _logic.GetPratica(1);
            Assert.Equal("Antonio", receivedPratica.Utente.Nome);
            Assert.Equal("test2", receivedPratica.AllegatoPratica);
        }

        [Fact]
        public void ProgressPraticaTest()
        {
            Pratica nuovaPratica = new Pratica();
            nuovaPratica.AllegatoPratica = "test";
            nuovaPratica.Utente = new Utente
            {
                CodiceFiscale = "CFTEST12345",
                Cognome = "Cognome",
                Nome = "Nome",
                DataDiNascita = DateTime.Now,
            };

            _logic.CreateNewPratica(nuovaPratica);

            Pratica receivedPratica = _logic.GetPratica(1);
            Assert.Equal(StatoPratica.Created, receivedPratica.StatoPratica);

            _logic.ProgressPratica(1);

            Assert.Equal(StatoPratica.Working, receivedPratica.StatoPratica);

            _logic.ProgressPratica(1);

            Assert.Equal(StatoPratica.Completed, receivedPratica.StatoPratica);
        }
    }
}
