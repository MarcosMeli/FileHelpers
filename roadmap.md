---
layout: default
title: Roadmap
permalink: /roadmap/
description: Plans for upcoming releases
---

We have planned two releases: **Release 3.2** in the next months and
**Release 4.0** some months later. We want to publish the fixed bugs and added
features we received through pull requests by the community. We also want to
improve the architecture of the library and simplify the maintenance of the
FileHelpers library.

There is a need to make the library usable with .NET Core. Still
we want to support Mono and the .NET Framework. To target all these three
runtimes, **.NET Standard** came up.

Since the last release of FileHelpers, almost two years have gone. The .NET
Standard 1.6 support for Reflection is much smaller compared to the .NET
Framework. It is uncertain if a stripped down version of FileHelpers would be
of any use. .NET Standard 2.0 is [scheduled for Q3
2017](https://github.com/dotnet/core/blob/master/roadmap.md), which is some more
months to go.

### Release 3.2

1. Will contain the code in the master branch.
2. Will contain some of the bug fixes and minors feature enhancements
3. Might drop the support for the .NET Framework 2.0 because of the use of LINQ.
4. Might have internal changes in order to alleviate changes later to .NET
  Standard. Some types / methods not present in .NET Standard can be replaced by
  types / methods that are present in .NET Framework and .NET Standard.

### Release 4

1. Will also be based on the current code in master: the transition to .NET
  Standard will start again from the current master branch
1. We might try transitions to get more insight: how to make the gap smaller
  between the code targeting .NET Framework and .NET Standard.
1. Support for older .NET Frameworks and Mono (2.0, 3.5) is desirable but will
  require extra effort as these Frameworks do not support .NET Standard
1. Will have a much different API
   1. Mapping based on properties (not fields as now)
1. Some features  will be removed such as
   1. Excel support (is based on COM Interop)
   1. DataLink
   1. Support for DataTable
