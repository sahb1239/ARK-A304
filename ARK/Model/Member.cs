﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARK.Model.XML;

namespace ARK.Model
{
    public class Member
    {
        public Member()
        {
        }

        public Member(XMLMembers.datarootAktiveMedlemmer memberXML)
        {
            this.MemberNumber = Convert.ToInt32(memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.MedlemsNr));
            this.ID = Convert.ToInt32(memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.ID));
            this.FirstName = (string)memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.Fornavn);
            this.LastName = (string)memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.Efternavn);
            this.Address1 = (string)memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.Adresse1);
            this.Address2 = (string)memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.Adresse2);
            this.ZipCode = Convert.ToInt32(memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.PostNr));
            this.City = (string)memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.By);
            this.Phone = (string)memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.Telefon);
            //this.PhoneWork = (string)memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.TelefonArbejde);
            this.Cellphone = (string)memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.TelefonMobil);
            this.Email1 = (string)memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.EMail);
            this.Email2 = (string)memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.EMail2);
            this.Birthday = Convert.ToDateTime(memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.Fødselsdato));
            this.Released = Convert.ToInt32(memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.Frigivet)) == 1 ? true : false;
            this.SwimmingTest = Convert.ToInt32(memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.Svømmeprøve)) == 1 ? true : false;
            this.ShortTripCox = Convert.ToInt32(memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.Korttursstyrmand)) == 1 ? true : false;
            this.LongTripCox = Convert.ToInt32(memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.Langtursstyrmand)) == 1 ? true : false;
            this.MayUseSculler = Convert.ToInt32(memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.Scullerret)) == 1 ? true : false;
            this.MayUseOutrigger = Convert.ToInt32(memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.Outriggerret)) == 1 ? true : false;
            this.MayUseKayak = Convert.ToInt32(memberXML.GetObjFromName(XMLMembers.ItemsChoiceType.Kajakret)) == 1 ? true : false;
        }

        public int MemberNumber { get; set; }
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string PhoneWork { get; set; }
        public string Cellphone { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public DateTime Birthday { get; set; }
        public bool Released { get; set; }
        public bool SwimmingTest { get; set; }
        public bool ShortTripCox { get; set; }
        public bool LongTripCox { get; set; }
        public bool MayUseSculler { get; set; }
        public bool MayUseOutrigger { get; set; }
        public bool MayUseKayak { get; set; }
    }
}
