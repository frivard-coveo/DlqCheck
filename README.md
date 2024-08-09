# DlqCheck

## Usage to read messages from DLQ

```
DlqCheck -q "ndev-queue-name-here"
```

you can also specify the maximum number of messages to read from the queue

```
DlqCheck -q "ndev-queue-name-here" -c 5
```

## Usage to read messages from DLQ and delete them

```
DlqCheck -q "ndev-queue-name-here" -d
```

## Usage to read messages from DLQ and redrive to original queue

```
DlqCheck -q "ndev-queue-name-here" -r
```

## for more information

```
DlqCheck -h
```


# How to test locally

- Make the changes in the code, and commit the changes.
- `dotnet pack` (you should see something like `Successfully created package 'D:\dev\DlqCheck\nupkg\DlqCheck.1.0.3.nupkg'.`)
- `dotnet nuget push .\nupkg\DlqCheck.1.0.3.nupkg --source Local`  (note that for this to work, you need to have a local nuget source setup)
- if the tool is already installed locally, `dotnet tool update -g DlqCheck`.
- if the tool is not installed, `dotnet tool install -g DlqCheck`.