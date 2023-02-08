FROM jboss/keycloak:latest

COPY mus-realm.json /tmp/mus-realm.json
ENV KEYCLOAK_IMPORT /tmp/mus-realm.json
ENV KEYCLOAK_IMPORT_REALM ${KEYCLOAK_IMPORT_REALM:-mus-realm.json}
ADD ${KEYCLOAK_IMPORT_REALM} /opt/jboss/keycloak/

ENTRYPOINT [ "/opt/jboss/tools/docker-entrypoint.sh" ]
CMD ["-b", "0.0.0.0"]
