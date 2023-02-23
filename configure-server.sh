set -m

/entrypoint.sh couchbase-server &

sleep 15

# Setup initial cluster/ Initialize Node
couchbase-cli cluster-init -c 127.0.0.1 --cluster-name FootballCluster --cluster-username Administrator \
  --cluster-password password --services data,index,query,fts --cluster-ramsize 256 --cluster-index-ramsize 256 \
  --cluster-fts-ramsize 256 --index-storage-setting default

# Setup Administrator username and password
curl -v http://127.0.0.1:8091/settings/web -d port=8091 -d username=Administrator -d password=password

sleep 15

# Setup Bucket
couchbase-cli bucket-create -c 127.0.0.1:8091 --username Administrator \
  --password password --bucket football-bucket --bucket-type couchbase \
  --bucket-ramsize 256

sleep 15

# Setup RBAC user using CLI
couchbase-cli user-manage -c 127.0.0.1:8091 --username Administrator --password password \
  --set --rbac-username admin --rbac-password password --rbac-name "admin" \
  --roles bucket_full_access[*],bucket_admin[*] --auth-domain local

# Setup Collection
couchbase-cli collection-manage -c 127.0.0.1:8091 \
  --username Administrator --password password \
  --bucket football-bucket --create-collection _default.player-collection

# Setup Collection
couchbase-cli collection-manage -c 127.0.0.1:8091 \
  --username Administrator --password password \
  --bucket football-bucket --create-collection _default.team-collection

sleep 15

# Add player data
cbc create -u Administrator -P password david_de_gea \
  -U couchbase://localhost/football-bucket/default/player-collection \
  -V '{
      "name": "David de Gea",
      "height": 192,
      "age": 32
    }'

#cbc create -u Administrator -P password luke_shaw \
#  -U couchbase://localhost/football-bucket \
#  -V '{
#        "name": "Luke Shaw",
#        "height": 150,
#        "age": 27
#      }'
cbc n1ql -u Administrator -P password -U couchbase://localhost \
'INSERT INTO `football-bucket`._default.`player-collection` (key,value) VALUES ("luke_shaw",{"name": "Luke Shaw","height": 150,"age": 27}) RETURNING *'



#cbc create -u Administrator -P password mason_mount \
#  -U couchbase://localhost/football-bucket._default.player-collection \
#  -V '{
#        "name": "Mason Mount",
#        "height": 181,
#        "age": 24
#      }'
#
#cbc create -u Administrator -P password raheem_sterling \
#  -U couchbase://localhost/football-bucket._default.player-collection \
#  -V '{
#        "name": "Raheem Sterling",
#        "height": 172,
#        "age": 28
#      }'

fg 1
