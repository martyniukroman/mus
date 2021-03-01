FROM jboss/keycloak:latest

COPY realm.json /tmp/realm.json
ENV KEYCLOAK_IMPORT /tmp/realm.json
ENV KEYCLOAK_IMPORT_REALM ${KEYCLOAK_IMPORT_REALM:-realm.json}
ADD ${KEYCLOAK_IMPORT_REALM} /opt/jboss/keycloak/
