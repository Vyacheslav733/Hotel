using HotelBusinessLogic.MailWorker;
using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using Microsoft.AspNetCore.Mvc;

namespace HotelRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly ILogger _logger;
        private readonly IReportOrganiserLogic _reportOrganiserLogic;
        private readonly IReportHeadwaiterLogic _reportHeadwaiterLogic;
		private readonly AbstractMailWorker _mailWorker;

		public ReportController(ILogger<ReportController> logger, IReportOrganiserLogic reportOrganiserLogic, IReportHeadwaiterLogic reportHeadwaiterLogic, AbstractMailWorker mailWorker)
        {
            _logger = logger;
            _reportOrganiserLogic = reportOrganiserLogic;
            _reportHeadwaiterLogic = reportHeadwaiterLogic;
			_mailWorker = mailWorker;
		}

        [HttpPost]
        public void CreateOrganiserReportToPdfFile(ReportOrganiserBindingModel model)
        {
            try
            {
                _reportOrganiserLogic.SaveMembersToPdfFile(new ReportOrganiserBindingModel
                {
                    DateFrom = model.DateFrom,
                    DateTo = model.DateTo,
                    OrganiserId = model.OrganiserId,
                    FileName = "C:\\Reports\\pdffile.pdf",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания отчета");
                throw;
            }
        }

		[HttpPost]
		public void SendPdfToMail(MailSendInfoBindingModel  model)
		{
			try
			{
				_mailWorker.MailSendAsync(model);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка отправки письма");
				throw;
			}
		}

		[HttpPost]
        public void CreateOrganiserReportToWordFile(ReportOrganiserBindingModel model)
        {
            try
            {
                _reportOrganiserLogic.SaveMemberConferenceToWordFile(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания отчета");
                throw;
            }
        }

        [HttpPost]
        public void CreateOrganiserReportToExcelFile(ReportOrganiserBindingModel model)
        {
            try
            {
                _reportOrganiserLogic.SaveMemberConferenceToExcelFile(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания отчета");
                throw;
            }
        }

        [HttpPost]
        public void CreateHeadwaiterReportToWordFile(ReportHeadwaiterBindingModel model)
        {
            try
            {
                _reportHeadwaiterLogic.SaveLunchRoomToWordFile(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания отчета");
                throw;
            }
        }

        [HttpPost]
        public void CreateHeadwaiterReportToExcelFile(ReportHeadwaiterBindingModel model)
        {
            try
            {
                _reportHeadwaiterLogic.SaveLunchRoomToExcelFile(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания отчета");
                throw;
            }
        }

        [HttpPost]
        public void CreateHeadwaiterReportToPdfFile(ReportHeadwaiterBindingModel model)
        {
            try
            {
                _reportHeadwaiterLogic.SaveLunchesToPdfFile(new ReportHeadwaiterBindingModel
                {
                    FileName = "C:\\Reports\\pdffile.pdf",
                    DateFrom = model.DateFrom,
                    DateTo = model.DateTo,
                    HeadwaiterId = model.HeadwaiterId,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания отчета");
                throw;
            }
        }
    }
}
