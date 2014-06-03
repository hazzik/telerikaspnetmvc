// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Extensions;

    using Moq;
    using Xunit;

    public class ClientSideObjectWriterTests
    {
        private const string Id = "myId";
        private const string Type = "myObject";

        private readonly Mock<TextWriter> _writer;
        private readonly ClientSideObjectWriter _objectWriter;

        public ClientSideObjectWriterTests()
        {
            _writer = new Mock<TextWriter>();
            _objectWriter = new ClientSideObjectWriter(Id, Type, _writer.Object);
        }

        [Fact]
        public void Start_should_write_id_type_and_opening_bracket()
        {
            _writer.Setup(w => w.Write("jQuery('#{0}').{1}(".FormatWith(Id, Type))).Verifiable();

            _objectWriter.Start();

            _writer.Verify();
        }

        [Fact]
        public void Start_should_throw_exception_when_called_more_than_once()
        {
            _objectWriter.Start();

            Assert.Throws<InvalidOperationException>(() => _objectWriter.Start());
        }

        [Fact]
        public void Append_should_write_json_start_when_previously_not_appended()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            _objectWriter.Start()
                         .Append("key:'value'");

            _writer.Verify(w => w.Write("{"));
            _writer.Verify(w => w.Write("key:'value'"));
        }

        [Fact]
        public void Append_should_write_comma_when_previously_appended()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            _objectWriter.Start()
                         .Append("key1:'value1'")
                         .Append("key2:'value2'");

            _writer.Verify(w => w.Write(", "));
            _writer.Verify(w => w.Write("key2:'value2'"));
        }

        [Fact]
        public void Append_should_throw_exception_if_begin_is_not_called_previously()
        {
            Assert.Throws<InvalidOperationException>(() => _objectWriter.Append("foo:'bar'"));
        }

        [Fact]
        public void Should_be_able_to_append_string_value()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            _objectWriter.Start()
                         .Append("foo", "bar");

            _writer.Verify(w => w.Write("foo:'bar'"));
        }

        [Fact]
        public void Should_be_able_to_append_string_value_with_special_characters()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            _objectWriter.Start().Append("foo", "\r\tf\"\'<a>\\adw\n\b\f");

            _writer.Verify();
        }

        [Fact]
        public void Should_be_able_to_append_nullable_integer_value()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            _objectWriter.Start()
                         .Append("foo", (int?) 5);

            _writer.Verify(w => w.Write("foo:5"));
        }

        [Fact]
        public void Should_be_able_to_append_integer_value_with_default_value()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            _objectWriter.Start()
                         .Append("foo", 5, 0);

            _writer.Verify(w => w.Write("foo:5"));
        }

        [Fact]
        public void Should_be_able_to_append_integer_value_without_default_value()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            _objectWriter.Start()
                         .Append("foo", 0);

            _writer.Verify(w => w.Write("foo:0"));
        }

        [Fact]
        public void Should_be_able_to_append_boolean_with_default_value()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            _objectWriter.Start()
                         .Append("foo", true, false);

            _writer.Verify(w => w.Write("foo:true"));
        }

        [Fact]
        public void Should_be_able_to_append_boolean_without_default_value()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            _objectWriter.Start()
                         .Append("foo", true);

            _writer.Verify(w => w.Write("foo:true"));
        }

        [Fact]
        public void Should_be_able_to_append_nullable_date_time_value()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            _objectWriter.Start()
                         .Append("foo", (DateTime?) new DateTime(2000, 1, 1, 23, 59, 59, 999));

            _writer.Verify(w => w.Write("foo:new Date(2000,00,01,23,59,59,999)"));
        }

        [Fact]
        public void Should_be_able_to_append_date_time_value()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            _objectWriter.Start()
                         .Append("foo", new DateTime(2000, 1, 1, 23, 59, 59, 999));

            _writer.Verify(w => w.Write("foo:new Date(2000,00,01,23,59,59,999)"));
        }

        [Fact]
        public void Should_be_able_to_append_action()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            _objectWriter.Start()
                         .Append("foo", () => {});

            _writer.Verify(w => w.Write("foo:"));
        }

        [Fact]
        public void Should_be_able_to_append_integer_list()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            IList<int> list = new List<int>{ 1, 2, 3};

            _objectWriter.Start()
                         .Append("foo", list);

            _writer.Verify(w => w.Write("foo:[1, 2, 3]"));
        }

        [Fact]
        public void Should_be_able_to_append_string_list()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            IList<string> list = new List<string> { "foo", "bar" };

            _objectWriter.Start()
                         .Append("dummy", list);

            _writer.Verify(w => w.Write("dummy:['foo', 'bar']"));
        }

        [Fact]
        public void Should_be_able_to_append_enum_with_default_value()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            _objectWriter.Start()
                         .Append("dummy", DummyEnum.Bar, DummyEnum.Foo);

            _writer.Verify(w => w.Write("dummy:'bar'"));
        }

        [Fact]
        public void Should_be_able_to_append_enum_without_default_value()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            _objectWriter.Start()
                         .Append("dummy", DummyEnum.Foo);

            _writer.Verify(w => w.Write("dummy:'foo'"));
        }

        [Fact]
        public void Complete_should_write_json_end_when_when_previously_appended()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            _objectWriter.Start()
                         .Append("key", "value")
                         .Complete();

            _writer.Verify(w => w.Write("}"));
        }

        [Fact]
        public void Complete_should_write_closing_Id_and_type_bracket()
        {
            _writer.Setup(w => w.Write(It.IsAny<string>())).Verifiable();

            _objectWriter.Start()
                         .Complete();

            _writer.Verify(w => w.Write(");"));
        }

        [Fact]
        public void Should_be_able_to_proceed_again_once_Complete_is_called()
        {
            _objectWriter.Start().Append("foo", "bar").Complete();

            Assert.DoesNotThrow(() => _objectWriter.Start().Append("baz", "yeda").Complete());
        }
    }

    public enum DummyEnum
    {
        [ClientSideEnumValue("'foo'")]
        Foo = 0,
        [ClientSideEnumValue("'bar'")]
        Bar = 1
    }
}