FROM jboss/keycloak:latest

USER root
ADD docker-entrypoint.sh /opt/jboss/
RUN chmod +x /opt/jboss/docker-entrypoint.sh
RUN mkdir /opt/jboss/auth-data/

EXPOSE 8080

ENTRYPOINT [ "/opt/jboss/docker-entrypoint.sh" ]

CMD ["-b", "0.0.0.0", "-bmanagement", "0.0.0.0", "-Dkeycloak.import=/opt/jboss/keycloak-demo/keycloak/testrealm.json"]