DECLARE @Ids table (Id uniqueidentifier PRIMARY KEY)
INSERT INTO @Ids 
	SELECT t.Id 
	FROM (SELECT Id, ROW_NUMBER() OVER (PARTITION BY EffectiveDate ORDER BY Id) [rn]
		FROM rates.[Table]) AS t
	WHERE t.rn <> 1

DELETE FROM rates.CurrencyRate WHERE TableId IN (SELECT Id FROM @Ids)
DELETE FROM rates.[Table] WHERE Id in (SELECT Id FROM @Ids)


--SELECT * FROM rates.[Table] WHERE Id in ('BEE3CE1F-7217-4149-BEED-5F70DFA4D8B5'
--,'43E6119A-C593-4B6A-B5B4-A2B0D6990568')