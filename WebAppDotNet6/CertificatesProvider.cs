using System.Security.Cryptography.X509Certificates;
namespace WebAppDotNet6
{
    public class CertificatesProvider
    {
        public List<string> GetCertificatesSubjectName()
        {
            List<string> subjectNames = new List<string>();
            var currentUserCerts = GetCurrentUserCertLists(StoreLocation.CurrentUser, StoreName.My);
            subjectNames.Add("####CurrentUserCertificatesName####");
            foreach (CertInfo certInfo in currentUserCerts)
            {
                subjectNames.Add($"StoreName = My, SubjectName: {certInfo.Subject}");
            }

            subjectNames.Add("####LocalMachineCertificatesName####");

            var localMachineCerts = GetCurrentUserCertLists(StoreLocation.LocalMachine, StoreName.My);
            foreach (CertInfo certInfo in localMachineCerts)
            {
                subjectNames.Add($"StoreName = My, SubjectName: {certInfo.Subject}");
            }

            var intermediateCA = GetCurrentUserCertLists(StoreLocation.LocalMachine, StoreName.CertificateAuthority);
            foreach (CertInfo certInfo in intermediateCA)
            {
                subjectNames.Add($"StoreName = Intermediate Certificate Authority (a.k.a. CertificateAuthority), SubjectName: {certInfo.Subject}");
            }

            var rootCA = GetCurrentUserCertLists(StoreLocation.LocalMachine, StoreName.Root);
            foreach (CertInfo certInfo in rootCA)
            {
                subjectNames.Add($"StoreName = Root Certificate Authority (a.k.a. Root), SubjectName: {certInfo.Subject}");
            }

            return subjectNames;
        }

        private IEnumerable<CertInfo> GetCurrentUserCertLists(StoreLocation storeLocation, StoreName storeName)
        {
            List<CertInfo> certList = new List<CertInfo>();
            X509Store certStore = new X509Store(storeName, storeLocation);
            // Try to open the store.
            certStore.Open(OpenFlags.ReadOnly);
            // Find the certificate that matches the thumbprint.
            X509Certificate2Collection certCollection = certStore.Certificates;
            certStore.Close();

            if (certCollection?.Count > 0)
            {
                foreach (X509Certificate2 cert in certCollection)
                {
                    CertInfo certInfo = new CertInfo
                    {
                        Thumbprint = cert.Thumbprint,
                        Subject = cert.Subject,
                        EffectiveDate = cert.GetEffectiveDateString(),
                        ExpirationDate = cert.GetExpirationDateString()
                    };

                    certList.Add(certInfo);
                }
            }

            return certList;
        }

        private class CertInfo
        {
            public string Thumbprint { get; set; }
            public string Subject { get; set; }
            public string EffectiveDate { get; set; }
            public string ExpirationDate { get; set; }
        }
    }
}
