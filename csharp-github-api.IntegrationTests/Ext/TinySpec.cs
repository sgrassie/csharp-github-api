// ==============================================================================
// 
// Fervent Coder Copyright © 2011 - Released under the Apache 2.0 License
// 
// Copyright 2007-2008 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
//
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
// ==============================================================================

#if NUNIT
using NUnit.Framework;
#else
using TestFixture = Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using TestFixtureSetUp = Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute;
using TestFixtureTearDown = Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute;
using SetUp = Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

using System;

namespace GitHubAPI.IntegrationTests.Ext
{

    [TestFixture]
    public abstract class TinySpec
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            Context();
            Because();
        }

        public abstract void Context();

        public abstract void Because();

        [TestFixtureTearDown]
        public void TearDown()
        {
            AfterObservations();
        }

        public virtual void AfterObservations() {}
    }

    //Note: Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute which is what ths should inherit from in VS testing is sealeds
    public class ObservationAttribute : NUnit.Framework.TestAttribute {}
    public class FactAttribute : NUnit.Framework.TestAttribute { }
    
    public class ConcernForAttribute : Attribute
    {
        public string Name { get; set; }
        
        public ConcernForAttribute(string name)
        {
            Name = name;
        }
    }
    
}