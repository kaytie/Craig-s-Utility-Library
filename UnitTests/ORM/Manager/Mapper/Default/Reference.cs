﻿/*
Copyright (c) 2014 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVReferenceED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

using System.Collections.Generic;
using System.Data;
using Utilities.ORM.BaseClasses;
using Utilities.ORM.Interfaces;
using Utilities.ORM.Manager.QueryProvider.Interfaces;
using Utilities.ORM.Manager.Schema.Default.Database;
using Xunit;

namespace UnitTests.ORM.Manager.Mapper.Default
{
    public class Reference : DatabaseBaseClass
    {
        public Reference()
            : base()
        {
            //var Temp = Utilities.IoC.Manager.Bootstrapper;
        }

        [Fact]
        public void CascadeDelete()
        {
            TestClass TempObject = new TestClass();
            TempObject.A = "ASDF";
            TempObject.ID = 1;
            Utilities.ORM.Manager.Mapper.Default.Reference<TestClass, string> TestObject = new Utilities.ORM.Manager.Mapper.Default.Reference<TestClass, string>(x => x.A, null);
            IBatch Result = TestObject.CascadeDelete(TempObject, new Utilities.ORM.Manager.SourceProvider.Manager().GetSource("ReferenceTest"));
            Assert.NotNull(Result);
            Assert.Equal("", Result.ToString());
            Assert.Equal(0, Result.CommandCount);
        }

        [Fact]
        public void CascadeJoinsDelete()
        {
            TestClass TempObject = new TestClass();
            TempObject.A = "ASDF";
            TempObject.ID = 1;
            Utilities.ORM.Manager.Mapper.Default.Reference<TestClass, string> TestObject = new Utilities.ORM.Manager.Mapper.Default.Reference<TestClass, string>(x => x.A, new TestClassMapping());
            TestObject.ForeignMapping = new TestClassMapping();
            IBatch Result = TestObject.CascadeJoinsDelete(TempObject, new Utilities.ORM.Manager.SourceProvider.Manager().GetSource("ReferenceTest"));
            Assert.NotNull(Result);
            Assert.Equal("", Result.ToString());
            Assert.Equal(0, Result.CommandCount);
        }

        [Fact]
        public void CascadeJoinsSave()
        {
            TestClass TempObject = new TestClass();
            TempObject.A = "ASDF";
            TempObject.ID = 1;
            Utilities.ORM.Manager.Mapper.Default.Reference<TestClass, string> TestObject = new Utilities.ORM.Manager.Mapper.Default.Reference<TestClass, string>(x => x.A, new TestClassMapping());
            TestObject.ForeignMapping = new TestClassMapping();
            IBatch Result = TestObject.CascadeJoinsSave(TempObject, new Utilities.ORM.Manager.SourceProvider.Manager().GetSource("ReferenceTest"));
            Assert.NotNull(Result);
            Assert.Equal("", Result.ToString());
            Assert.Equal(0, Result.CommandCount);
        }

        [Fact]
        public void CascadeSave()
        {
            TestClass TempObject = new TestClass();
            TempObject.A = "ASDF";
            TempObject.ID = 1;
            Utilities.ORM.Manager.Mapper.Default.Reference<TestClass, string> TestObject = new Utilities.ORM.Manager.Mapper.Default.Reference<TestClass, string>(x => x.A, new TestClassMapping());
            IBatch Result = TestObject.CascadeSave(TempObject, new Utilities.ORM.Manager.SourceProvider.Manager().GetSource("ReferenceTest"));
            Assert.NotNull(Result);
            Assert.Equal("", Result.ToString());
            Assert.Equal(0, Result.CommandCount);
        }

        [Fact]
        public void Create()
        {
            Utilities.ORM.Manager.Mapper.Default.Reference<TestClass, string> TestObject = new Utilities.ORM.Manager.Mapper.Default.Reference<TestClass, string>(x => x.A, new TestClassMapping());
            Assert.False(TestObject.AutoIncrement);
            Assert.False(TestObject.Cascade);
            Assert.NotNull(TestObject.CompiledExpression);
            Assert.NotNull(TestObject.DefaultValue);
            Assert.Equal(null, TestObject.DefaultValue());
            Assert.Equal("_ADerived", TestObject.DerivedFieldName);
            Assert.NotNull(TestObject.Expression);
            Assert.Equal("A_", TestObject.FieldName);
            Assert.Null(TestObject.ForeignMapping);
            Assert.False(TestObject.Index);
            Assert.NotNull(TestObject.Mapping);
            Assert.Equal(100, TestObject.MaxLength);
            Assert.Equal("A", TestObject.Name);
            Assert.False(TestObject.NotNull);
            Assert.Equal("TestClass_", TestObject.TableName);
            Assert.Equal(typeof(string), TestObject.Type);
            Assert.False(TestObject.Unique);
        }

        private class Database : IDatabase
        {
            public bool Audit
            {
                get { return false; }
            }

            public string Name
            {
                get { return "ReferenceTest"; }
            }

            public int Order
            {
                get { return 0; }
            }

            public bool Readable
            {
                get { return false; }
            }

            public bool Update
            {
                get { return false; }
            }

            public bool Writable
            {
                get { return false; }
            }
        }

        private class TestClass
        {
            public string A { get; set; }

            public int ID { get; set; }
        }

        private class TestClassMapping : MappingBaseClass<TestClass, Database>
        {
            public TestClassMapping()
            {
                Reference(x => x.A);
                ID(x => x.ID).SetFieldName("ID").SetAutoIncrement();
            }
        }
    }
}