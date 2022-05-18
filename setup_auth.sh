export WIKI_AUTH_SECRET
WIKI_AUTH_SECRET="$(source generate_secret.sh)"
setx WIKI_AUTH_SECRET $WIKI_AUTH_SECRET
echo "Sua chave secreta foi gerada e armazenada na variável de ambiente WIKI_AUTH_SECRET"

export WIKI_AUTH_SALT
WIKI_AUTH_SALT="$(source generate_secret.sh)"
setx WIKI_AUTH_SALT $WIKI_AUTH_SECRET
echo "Sua chave secreta foi gerada e armazenada na variável de ambiente WIKI_AUTH_SALT"