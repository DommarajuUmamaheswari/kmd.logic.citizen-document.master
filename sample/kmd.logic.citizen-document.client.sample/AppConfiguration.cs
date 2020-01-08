﻿using Kmd.Logic.Identity.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace kmd.logic.citizen_document.client.sample
{
    internal class AppConfiguration
    {

        public LogicTokenProviderOptions TokenProvider { get; set; } = new LogicTokenProviderOptions();
               
        public CitizenDocumentOptions Citizen { get; set; } = new CitizenDocumentOptions();

        public string SubscriptionId { get; set; } = "3bef5d8f-1315-4a54-993e-b51a7d918340";
        public string ConfiguartionId { get; set; } = "f555d0c7-563a-4c11-b3c6-fe43987391d9";
        public string Cpr { get; set; } = "1403532411";
        public int RetentionPeriodInDays { get; set; } = 3;
        public string DocumentType { get; set; } = "CitizenDocument";            
     
        public Stream Document { get; set; } = File.OpenRead("TestPdfInA4Format.pdf");
        public string SendingSystem { get; set; } = "test";
        public string SendDocumentType { get; set; } = "alm brev";
        public string title { get; set; } = "test";


    }
}
