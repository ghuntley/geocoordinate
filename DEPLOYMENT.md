# Deploying a new release


When we're ready to deploy a new release, we need to do the following steps:

* Create a branch named `release`.
* Update [`ReleaseNotes.md`](ReleaseNotes.md). Note that the format is
important as we parse the version out and use that for the NuGet packages.
* Push the branch to GitHub and create a pull request. This will kick off the
AppVeyor build of the NuGet package with this new version. If you're impatient, you can run `.\build CreatePackages` and get the packages locally. If your having issues creating a package read: https://github.com/octokit/octokit.net/issues/899
* Test!
* When you're satisfied with this release, push the package to NuGet.
* Create a tag `git tag v#.#.#`. For example, to create a tag for 1.0.0 
`git tag v1.0.0`
* Push the tag to the server. `git push --tags`
* Accept the pull request.
* Create a [new release](https://github.com/ghuntley/geocoordinate/releases/new)
using the tag you just created and pasting in the release notes you just wrote up