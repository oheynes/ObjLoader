﻿using FluentAssertions;
using NUnit.Framework;
using ObjLoader.Loader.Data;

namespace ObjLoader.Loader.TypeParsers
{
    [TestFixture]
    public class FaceParserTests
    {
        private FaceGroupMock _faceGroupMock;
        private FaceParser _faceParser;

        [SetUp]
        public void SetUp()
        {
            _faceGroupMock = new FaceGroupMock();

            _faceParser = new FaceParser(_faceGroupMock);
        }

        [Test]
        public void CanParse_returns_true_on_face_line()
        {
            const string groupKeyword = "f";

            bool canParse = _faceParser.CanParse(groupKeyword);
            canParse.Should().BeTrue();
        }

        [Test]
        public void CanParse_returns_false_on_non_normal_line()
        {
            const string invalidKeyword = "vt";

            bool canParse = _faceParser.CanParse(invalidKeyword);
            canParse.Should().BeFalse();
        }

        [Test]
        public void Parses_normal_line_correctly_1()
        {
            const string faceLine = "f 1 2 3";
            _faceParser.Parse(faceLine);

            var parsedFace = _faceGroupMock.ParsedFace;

            parsedFace[0].VertexIndex.Should().Be(1);
            parsedFace[0].TextureIndex.Should().Be(0);
            parsedFace[0].NormalIndex.Should().Be(0);

            parsedFace[1].VertexIndex.Should().Be(2);
            parsedFace[1].TextureIndex.Should().Be(0);
            parsedFace[1].NormalIndex.Should().Be(0);

            parsedFace[2].VertexIndex.Should().Be(3);
            parsedFace[2].TextureIndex.Should().Be(0);
            parsedFace[2].NormalIndex.Should().Be(0);
        }

        [Test]
        public void Parses_normal_line_correctly_2()
        {
            const string faceLine = "f 3/1 4/2 5/3";
            _faceParser.Parse(faceLine);

            var parsedFace = _faceGroupMock.ParsedFace;

            parsedFace[0].VertexIndex.Should().Be(3);
            parsedFace[0].TextureIndex.Should().Be(1);
            parsedFace[0].NormalIndex.Should().Be(0);

            parsedFace[1].VertexIndex.Should().Be(4);
            parsedFace[1].TextureIndex.Should().Be(2);
            parsedFace[1].NormalIndex.Should().Be(0);

            parsedFace[2].VertexIndex.Should().Be(5);
            parsedFace[2].TextureIndex.Should().Be(3);
            parsedFace[2].NormalIndex.Should().Be(0);
        }

        [Test]
        public void Parses_normal_line_correctly_3()
        {
            const string faceLine = "f 6/4/1 3/5/3 7/6/5";
            _faceParser.Parse(faceLine);

            var parsedFace = _faceGroupMock.ParsedFace;

            parsedFace[0].VertexIndex.Should().Be(6);
            parsedFace[0].TextureIndex.Should().Be(4);
            parsedFace[0].NormalIndex.Should().Be(1);

            parsedFace[1].VertexIndex.Should().Be(3);
            parsedFace[1].TextureIndex.Should().Be(5);
            parsedFace[1].NormalIndex.Should().Be(3);

            parsedFace[2].VertexIndex.Should().Be(7);
            parsedFace[2].TextureIndex.Should().Be(6);
            parsedFace[2].NormalIndex.Should().Be(5);
        }

        [Test]
        public void Parses_normal_line_correctly_4()
        {
            const string faceLine = "f 6//1 3//3 7//5";
            _faceParser.Parse(faceLine);

            var parsedFace = _faceGroupMock.ParsedFace;

            parsedFace[0].VertexIndex.Should().Be(6);
            parsedFace[0].TextureIndex.Should().Be(0);
            parsedFace[0].NormalIndex.Should().Be(1);

            parsedFace[1].VertexIndex.Should().Be(3);
            parsedFace[1].TextureIndex.Should().Be(0);
            parsedFace[1].NormalIndex.Should().Be(3);

            parsedFace[2].VertexIndex.Should().Be(7);
            parsedFace[2].TextureIndex.Should().Be(0);
            parsedFace[2].NormalIndex.Should().Be(5);
        }
    }

    public class FaceGroupMock : IFaceGroup
    {
        public Face ParsedFace { get; private set; }

        public Face GetFace(int i)
        {
            throw new System.NotImplementedException();
        }

        public void AddFace(Face face)
        {
            ParsedFace = face;
        }
    }
}