using Sample.Application.interfaces.Sample;

namespace Sample.Infrastructure
{
    public class SampleRepository :ISampleRepository
    {

        public static List<Domain.Sample> lstMembers = new List<Domain.Sample>()
        {
           new Domain.Sample{  Id =1 ,Name= "Kirtesh Shah", Type ="G" , Address="Vadodara"},
           new Domain.Sample{  Id =2 ,Name= "Mahesh Shah", Type ="S" , Address="Dabhoi"},
           new Domain.Sample{  Id =3 ,Name= "Nitya Shah", Type ="G" , Address="Mumbai"},
           new Domain.Sample{  Id =4 ,Name= "Dilip Shah", Type ="S" , Address="Dabhoi"},
           new Domain.Sample{  Id =5 ,Name= "Hansa Shah", Type ="S" , Address="Dabhoi"},
           new Domain.Sample{  Id =6 ,Name= "Mita Shah", Type ="G" , Address="Surat"}
        };

        public List<Domain.Sample> GetAllSamples()
        {
            return lstMembers;
        }
    }
}
