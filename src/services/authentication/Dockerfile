FROM quay.io/keycloak/keycloak:latest

USER root
ADD docker-entrypoint.sh /tmp/
RUN chmod +x /tmp/docker-entrypoint.sh
RUN mkdir /tmp/auth-data/
RUN chown 1000 /tmp/auth-data/
ENV KEYCLOACK_IMPORT_MODE IGNORE_EXISTING

EXPOSE 8080

ENTRYPOINT [ "/tmp/docker-entrypoint.sh" ]

CMD ["-b", "0.0.0.0", "-bmanagement", "0.0.0.0", "-Dkeycloak.import=/tmp/keycloak-demo/keycloak/testrealm.json"]