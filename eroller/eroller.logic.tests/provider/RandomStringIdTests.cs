using System.Collections.Generic;
using System.Linq;
using eroller.logic.provider;
using NUnit.Framework;

namespace eroller.logic.tests.provider
{
	[TestFixture]
	public class RandomStringIdTests
	{
		[Test, Repeat(500)]
		public void Generates_correct_id() {
			var id = RandomStringId.New();

			Assert.That(id.Length, Is.EqualTo(6));
			Assert.That(id.Any(ch => RandomStringId.LegalCharacters.Contains(ch)), Is.True);
		}

		[Test, Repeat(500)]
		public void Generated_ids_are_unique() {
			var listOfIds = new List<string>();
			for(var i = 0; i < 1000; i++) {
				var id = RandomStringId.New();
				Assert.That(listOfIds.Contains(id), Is.False);
				listOfIds.Add(id);
			}
		}
	}
}
