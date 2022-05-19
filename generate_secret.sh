rval=$(echo $RANDOM$RANDOM$RANDOM)
x=$(echo $rval | sha256sum)
echo ${x::-3}