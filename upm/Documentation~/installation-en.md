# Installation

## Table of Contents

- [Installation](#installation)
  - [Table of Contents](#table-of-contents)
  - [OpenUPM](#openupm)
    - [Unity Package Manager](#unity-package-manager)
    - [OpenUPM CLI](#openupm-cli)
  - [Unity Package Manager](#unity-package-manager-1)
    - [git url](#git-url)
    - [tarball](#tarball)

## OpenUPM

```url
https://openupm.com/packages/com.moroshka.derived-types.html
```

### Unity Package Manager

1. Open the Packages/manifest.json file in your project.
2. Add "scopedRegistries" if it doesn't exist yet:

``` json
{
  "scopedRegistries": [
    {
      "name": "OpenUPM",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.moroshka"
      ]
    }
  ],
  "dependencies": {
    "com.moroshka.derived-types": "#.#.#"
  }
}
```

### OpenUPM CLI

Install OpenUPM CLI (requires Node.js):

``` bash
npm install -g openupm-cli
```

In the project folder, run:

``` bash
openupm add com.moroshka.derived-types
```

## Unity Package Manager

### git url

1. Open Window → Package Manager.
2. Click + → Add package from git URL...
3. Enter the url and click Add.

```url
https://github.com/moroshka-dev/moroshka-derived-types.git?path=upm#v#.#.#
```

### tarball

1. Download the .tgz.gz file of the desired version
2. Open Window → Package Manager.
3. Click + → Add package from tarball...
4. Select the downloaded .tgz.gz file and click Open.

``` url
https://github.com/moroshka-dev/moroshka-derived-types/releases/
```
