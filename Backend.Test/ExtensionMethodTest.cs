using Backend.Util;

namespace Backend.Test;

public class ExtensionMethodTest
{
    [SetUp] public void Setup() { }

    [Test]
    public void TestIsVideoIdValid()
    {
        Assert.Multiple(() =>
                        {
                            Assert.That("abcdefghijk".IsVideoId(), Is.True);
                            Assert.That("12345678910".IsVideoId(), Is.True);
                            Assert.That("123456789-_".IsVideoId(), Is.True);
                            Assert.That("abc456789-_".IsVideoId(), Is.True);
                        });
    }

    [Test]
    public void TestIsVideoIdInvalid()
    {
        Assert.Multiple(() =>
                        {
                            Assert.That("".IsVideoId(), Is.False);
                            Assert.That("a".IsVideoId(), Is.False);
                            Assert.That(" ".IsVideoId(), Is.False);
                            Assert.That("           ".IsVideoId(), Is.False);
                            Assert.That("abcä56789-_".IsVideoId(), Is.False);
                            Assert.That("abc.56789-_".IsVideoId(), Is.False);
                            Assert.That("abcdefghijkj".IsVideoId(), Is.False);
                            Assert.That("abcdefghij ".IsVideoId(), Is.False);
                        });
    }

    [Test]
    public void TestIsInvalidYouTubeUrl()
    {
        Assert.Multiple(() =>
                        {
                            Assert.That("".IsInvalidYouTubeUrl(), Is.True);
                            Assert.That("a".IsInvalidYouTubeUrl(), Is.True);
                            Assert.That(" ".IsInvalidYouTubeUrl(), Is.True);
                            Assert.That("           ".IsInvalidYouTubeUrl(), Is.True);
                            Assert.That("youtube".IsInvalidYouTubeUrl(), Is.True);
                            Assert.That("youtu.be".IsInvalidYouTubeUrl(), Is.True);
                            Assert.That("ayoutu.be".IsInvalidYouTubeUrl(), Is.True);
                            Assert.That(" youtu.be".IsInvalidYouTubeUrl(), Is.True);
                            Assert.That("youtu.bea".IsInvalidYouTubeUrl(), Is.True);
                            Assert.That("youtu.be ".IsInvalidYouTubeUrl(), Is.True);
                            Assert.That("ayoutu.be/".IsInvalidYouTubeUrl(), Is.True);
                            Assert.That("youtube.com".IsInvalidYouTubeUrl(), Is.True);
                            Assert.That("youtube.com ".IsInvalidYouTubeUrl(), Is.True);
                            Assert.That("ayoutube.com ".IsInvalidYouTubeUrl(), Is.True);
                            Assert.That(" youtube.com ".IsInvalidYouTubeUrl(), Is.True);
                            Assert.That("ayoutube.com/ ".IsInvalidYouTubeUrl(), Is.True);
                            Assert.That("google.com".IsInvalidYouTubeUrl(), Is.True);
                            Assert.That("google.com/".IsInvalidYouTubeUrl(), Is.True);
                        });
    }

    [Test]
    public void TestIsValidYouTubeUrl()
    {
        Assert.Multiple(() =>
                        {
                            Assert.That("youtu.be/a".IsInvalidYouTubeUrl(), Is.False);
                            Assert.That(" youtu.be/a".IsInvalidYouTubeUrl(), Is.False);
                            Assert.That("youtu.be/test".IsInvalidYouTubeUrl(), Is.False);
                            Assert.That("youtube.com/a".IsInvalidYouTubeUrl(), Is.False);
                            Assert.That("youtube.com/a ".IsInvalidYouTubeUrl(), Is.False);
                            Assert.That(" youtube.com/a ".IsInvalidYouTubeUrl(), Is.False);
                            Assert.That("http://youtube.com/a".IsInvalidYouTubeUrl(), Is.False);
                            Assert.That("https://youtube.com/a".IsInvalidYouTubeUrl(), Is.False);
                            Assert.That("http://www.youtube.com/a".IsInvalidYouTubeUrl(), Is.False);
                            Assert.That("https://www.youtube.com/a".IsInvalidYouTubeUrl(), Is.False);
                            Assert.That("www.youtube.com/a".IsInvalidYouTubeUrl(), Is.False);
                            Assert.That("http://youtu.be/a".IsInvalidYouTubeUrl(), Is.False);
                            Assert.That("https://youtu.be/a".IsInvalidYouTubeUrl(), Is.False);
                            Assert.That("http://www.youtu.be/a".IsInvalidYouTubeUrl(), Is.False);
                            Assert.That("https://www.youtu.be/a".IsInvalidYouTubeUrl(), Is.False);
                            Assert.That("www.youtu.be/a".IsInvalidYouTubeUrl(), Is.False);
                        });
    }
}