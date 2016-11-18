//using Owin;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace CourseProject.App_Start
//{
//    public class Start
//    {
//        public void ConfigureAuth(IAppBuilder app)
//        {
//            app.UseTwitterAuthentication(new TwitterAuthenticationOptions
//            {
//                ConsumerKey = "iJVM5GqxMBBEyvz10Pxs3m6Lb",
//                ConsumerSecret = "6KlIfTtKvpjetodFkd10uyNGZ4ghM8pIy6ywdTFAcqIWUgy6rE",
//                BackchannelCertificateValidator = new CertificateSubjectKeyIdentifierValidator(new[]
//                {
//                    "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2 
//                    "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3 
//                    "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary Certification Authority - G5 
//                    "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4 
//                    "5168FF90AF0207753CCCD9656462A212B859723B", //DigiCert SHA2 High Assurance Server CA 
//                    "B13EC36903F8BF4701D498261A0802EF63642BC3" //DigiCert High Assurance EV Root CA 
//                })
//            });

//            // app.UseTwitterAuthentication(
//            //   consumerKey: "iJVM5GqxMBBEyvz10Pxs3m6Lb",
//            //   consumerSecret: "6KlIfTtKvpjetodFkd10uyNGZ4ghM8pIy6ywdTFAcqIWUgy6rE");

//            app.UseFacebookAuthentication(
//               appId: "1815580465355811",
//               appSecret: "c04bfe4d9cc6b130553581b0f8f64ca6");

//            app.UseVkontakteAuthentication("5676120", "Nu0vHe58WZdsBUv6aUr3", "{PERMISSIONS}");
//            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
//            //{
//            //    ClientId = "",
//            //    ClientSecret = ""
//            //});
//        }
//    }
//}