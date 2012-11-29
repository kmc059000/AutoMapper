using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace AutoMapper.UnitTests.Bug
{
    namespace KmcBug
    {
        [TestFixture]
        public class KMCBug
        {
            [Test]
            public void Example()
            {
                Mapper.CreateMap<SourceInterface, DestinationBase>().Include<SourceInterface, Destination>();

                var user = new Source()
                    {
                        ID = 1
                    };

                var result = Mapper.Map<Destination>(user); //throws NullReferenceException

                Assert.AreEqual(user.ID, result.ID);
            }

            [Test]
            public void Example2()
            {

                Mapper.CreateMap<SourceInterface, DestinationBase>();

                Mapper.CreateMap<SourceInterface, Destination>();

                var user = new Source()
                {
                    ID = 1
                };


                var result = Mapper.Map<Destination>(user); //does not throw

                Assert.AreEqual(user.ID, result.ID);
            }

            [Test]
            public void Example3()
            {

                Mapper.CreateMap<Source, Destination>();

                var user = new Source()
                {
                    ID = 1
                };


                var result = Mapper.Map<Destination>(user); //does not throw

                Assert.AreEqual(user.ID, result.ID);
            }
        }


        public interface SourceInterface
        {
            int ID { get; set; }
        }

        public class Source : SourceInterface
        {
            public int ID { get; set; }
        }

        public abstract class DestinationBase
        {
            protected DestinationBase()
            {
            }

            protected DestinationBase(SourceInterface source)
            {
                ID = source.ID;
            }

            public int ID { get; set; }
        }

        public class Destination : DestinationBase
        {
        }
    }
}
