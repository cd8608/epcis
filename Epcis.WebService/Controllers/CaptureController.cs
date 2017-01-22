﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Epcis.Domain.Exceptions;
using Epcis.XmlParser;
using Epcis.XmlParser.Validation;

namespace Epcis.WebService.Controllers
{
    public class CaptureController : ApiController
    {
        private readonly IDocumentValidator _documentValidator;
        private readonly IDocumentProcessor _documentProcessor;

        public CaptureController(IDocumentValidator documentValidator, IDocumentProcessor documentProcessor)
        {
            if (documentValidator == null) throw new ArgumentNullException(nameof(documentValidator));
            if (documentProcessor == null) throw new ArgumentNullException(nameof(documentProcessor));

            _documentValidator = documentValidator;
            _documentProcessor = documentProcessor;
        }

        public async Task<IHttpActionResult> Post()
        {
            try
            {
                var xmlBody = XDocument.Parse(await Request.Content.ReadAsStringAsync());
                _documentValidator.Validate(xmlBody);
                _documentProcessor.Process(xmlBody);

                return Ok();
            }
            catch (XmlSchemaValidationException schemaValidationException)
            {
                return BadRequest(schemaValidationException.Message);
            }
            catch (XmlException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (EventMapException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}