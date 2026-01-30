# Установка

## Содержание

- [Установка](#установка)
  - [Содержание](#содержание)
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

1. Открой файл Packages/manifest.json в проекте.
2. Добавь "scopedRegistries" если его ещё нет:

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

Установи OpenUPM CLI (нужен Node.js):

``` bash
npm install -g openupm-cli
```

В папке проекта выполни:

``` bash
openupm add com.moroshka.derived-types
```

## Unity Package Manager

### git url

1. Открыть Window → Package Manager.
2. Нажать на + → Add package from git URL...
3. Ввести url и нажать Add.

```url
https://github.com/moroshka-dev/moroshka-derived-types.git?path=upm#v#.#.#
```

### tarball

1. Скачать .tgz.gz нужной версии
2. Открыть Window → Package Manager.
3. Нажать на + → Add package from tarball...
4. Выбрать скачанный .tgz.gz файл и нажать Open.

``` url
https://github.com/moroshka-dev/moroshka-derived-types/releases/
```
