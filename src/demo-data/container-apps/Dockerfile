# Context is at folder release
# Use the same base image as your services
FROM liquibase/liquibase:4.28.0-alpine

WORKDIR /liquibase
# Copy the Liquibase configuration files
COPY db/liquibase/changelog.yaml /liquibase/changelog.yaml
COPY db/migrations /liquibase/migrations
# COPY liquibase.template.properties /liquibase/liquibase.properties

COPY db/liquibase/custom-entrypoint.sh /liquibase/generate_properties.sh

# Make the script executable and convert line endings
USER root
# RUN chmod +x /liquibase/generate_properties.sh
RUN chmod +x /liquibase/generate_properties.sh && \
    sed -i 's/\r$//' /liquibase/generate_properties.sh && \
    chmod 644 /liquibase/changelog.yaml && \
    sed -i 's/\r$//' /liquibase/changelog.yaml && \
    find /liquibase/migrations -name "*.sql" -exec chmod 644 {} \; && \
    find /liquibase/migrations -name "*.sql" -exec sed -i 's/\r$//' {} \; && \
    chown -R liquibase:root /liquibase/changelog.yaml /liquibase/migrations && \
    ls -la /liquibase/changelog.yaml

ENTRYPOINT [ "/liquibase/generate_properties.sh" ]


##command for run in local

#command to build the container
# docker build -f ./lqb.Dockerfile . -t test-lqb

#command to run the container
# docker run --network <replace-with-docker-container-name> \
#   -e DATABASE_URL=jdbc:postgresql://<replace-with-docker-container-name>:5432/postgres \
#   -e DATABASE_USER=postgres\
#   -e DATABASE_PASSWORD=postgres\
#   -e DATABASE_SCHEMA=GU_MINE\
#   -e LQB_COMMAND="update"\
#   test-lqb