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
