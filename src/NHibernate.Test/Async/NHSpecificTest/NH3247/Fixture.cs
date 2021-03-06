﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Linq;
using NHibernate.Linq;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH3247
{
	using System.Threading.Tasks;
	[TestFixture]
	public class FixtureAsync : BugTestCase
	{
		protected override void OnSetUp()
		{
			using (ISession session = OpenSession())
			using (ITransaction transaction = session.BeginTransaction())
			{
				var e1 = new Entity {Name = "Bob", Initial = 'B' };
				session.Save(e1);

				var e2 = new Entity {Name = "Sally", Initial = 'S' };
				session.Save(e2);

				session.Flush();
				transaction.Commit();
			}
		}

		protected override void OnTearDown()
		{
			using (ISession session = OpenSession())
			using (ITransaction transaction = session.BeginTransaction())
			{
				session.Delete("from System.Object");

				session.Flush();
				transaction.Commit();
			}
		}

		[Test]
		public async Task CharParameterValueShouldNotBeCachedAsync()
		{
			using (ISession session = OpenSession())
			using (session.BeginTransaction())
			{
				var result = await (session.Query<Entity>()
					.Where(e => e.Initial == 'B')
					.SingleAsync());

				Assert.AreEqual('B', result.Initial);

				result = await (session.Query<Entity>()
					.Where(e => e.Initial == 'S')
					.SingleAsync());

				Assert.AreEqual('S', result.Initial);
			}
		}
	}
}