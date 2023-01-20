SELECT 'ALTER SCHEMA webstxtech TRANSFER [' + SysSchemas.Name + '].[' + DbObjects.Name + '];'
FROM sys.Objects DbObjects
INNER JOIN sys.Schemas SysSchemas ON DbObjects.schema_id = SysSchemas.schema_id
WHERE SysSchemas.Name = 'dbo'
AND (DbObjects.Type IN ('U', 'P', 'V'))

ALTER SCHEMA webstxtech TRANSFER [dbo].[SeoToolkitSitemapPageType];
ALTER SCHEMA webstxtech TRANSFER [dbo].[SeoToolkitRobotsTxt];
ALTER SCHEMA webstxtech TRANSFER [dbo].[SeoToolkitSeoSettings];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoNode];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoPropertyData];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoRedirectUrl];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoRelation];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoRelationType];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoServer];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoUser];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoUser2NodeNotify];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoUser2UserGroup];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoUserGroup];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoUserGroup2App];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoUserGroup2Node];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoCreatedPackageSchema];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoUserGroup2NodePermission];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoTwoFactorLogin];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoUserLogin];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoUserStartNode];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoUserGroup2Language];
ALTER SCHEMA webstxtech TRANSFER [dbo].[cmsContentNu];
ALTER SCHEMA webstxtech TRANSFER [dbo].[cmsContentType];
ALTER SCHEMA webstxtech TRANSFER [dbo].[cmsContentType2ContentType];
ALTER SCHEMA webstxtech TRANSFER [dbo].[cmsContentTypeAllowedContentType];
ALTER SCHEMA webstxtech TRANSFER [dbo].[cmsDictionary];
ALTER SCHEMA webstxtech TRANSFER [dbo].[cmsDocumentType];
ALTER SCHEMA webstxtech TRANSFER [dbo].[cmsLanguageText];
ALTER SCHEMA webstxtech TRANSFER [dbo].[cmsMacro];
ALTER SCHEMA webstxtech TRANSFER [dbo].[cmsMacroProperty];
ALTER SCHEMA webstxtech TRANSFER [dbo].[cmsMember];
ALTER SCHEMA webstxtech TRANSFER [dbo].[cmsMember2MemberGroup];
ALTER SCHEMA webstxtech TRANSFER [dbo].[cmsMemberType];
ALTER SCHEMA webstxtech TRANSFER [dbo].[cmsPropertyType];
ALTER SCHEMA webstxtech TRANSFER [dbo].[cmsPropertyTypeGroup];
ALTER SCHEMA webstxtech TRANSFER [dbo].[cmsTagRelationship];
ALTER SCHEMA webstxtech TRANSFER [dbo].[cmsTags];
ALTER SCHEMA webstxtech TRANSFER [dbo].[cmsTemplate];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoAccess];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoAccessRule];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoAudit];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoCacheInstruction];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoConsent];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoContent];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoContentSchedule];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoContentVersion];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoContentVersionCleanupPolicy];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoContentVersionCultureVariation];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoDataType];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoDocument];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoDocumentCultureVariation];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoDocumentVersion];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoDomain];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoExternalLogin];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoExternalLoginToken];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoKeyValue];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoLanguage];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoLock];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoLog];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoLogViewerQuery];
ALTER SCHEMA webstxtech TRANSFER [dbo].[umbracoMediaVersion];