using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StreamProcessing.Models;
using StreamProcessing.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamProcessing.Tests
{
    // test class for IStream interface
    class StreamTest : IStream
    {
        private int _score;
        public StreamTest(int score)
        {
            _score = score;
        }
        public int Score(int outer)
        {
            return _score;
        }
    }

    // unit test using MStest
    [TestClass]
    public class StreamProcessUnitTest
    {
        private Mock<IStreamProcessService> _mockStreamProcessService;
        IStreamProcessService streamProcessService;

        [TestMethod]
        public void CheckGetScoreForStreamInput()
        {
            var group = streamProcessService.ParseGroup();
            var score = group.Score(0);
            Assert.AreEqual(4, score);
            Assert.IsNotNull(group);
            Assert.AreEqual(5, group.Score(1));
        }

        [TestInitialize]
        public void Initialize()
        {
            // mock service using Moq mocking library
            _mockStreamProcessService = new Mock<IStreamProcessService>();
            streamProcessService = _mockStreamProcessService.Object;

            var streams = new IStream[2];
            streams[0] = new StreamTest(1);
            streams[1] = new StreamTest(2);
            Group group = new Group(streams);

            // seperator tested method by setup ParseGroup function inside getscore
            _mockStreamProcessService.Setup(s => s.ParseGroup()).Returns(group);
        }
    }
}
