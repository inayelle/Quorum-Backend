using System.Threading.Tasks;
using Quorum.BusinessCore.Entities;
using Quorum.BusinessCore.Interfaces;

namespace Quorum.BusinessCore.Models.Challenge
{
	public sealed class ChallengeModel
	{
		private readonly ITestRepository           _tests;
		private readonly IChallengedTestRepository _challengedTests;

		public ChallengeModel(ITestRepository tests, IChallengedTestRepository challengedTests)
		{
			_tests           = tests;
			_challengedTests = challengedTests;
		}

		public async Task<int> ChallengeTest(ChallengedTest challengedTest)
		{
			var test = await _tests.GetAsync(challengedTest.SourceTestId);

			var challengeExecutor = new ChallengeExecutor();
			
			await challengeExecutor.ChallengeAsync(test, challengedTest);

			return await _challengedTests.Create(challengedTest);
		}
	}
}