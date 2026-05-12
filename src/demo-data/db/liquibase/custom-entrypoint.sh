#!/bin/bash
set -e

# Check if COMMAND is set
if [ -z "$LQB_COMMAND" ]; then
  echo "Error: LQB_COMMAND is not set."
  exit 1
fi

# Run Liquibase with the provided command
liquibase \
  --url=$LQB_DATABASE_URL \
  --username=$LQB_DATABASE_USER \
  --password=$LQB_DATABASE_PASSWORD \
  --defaultSchemaName=$LQB_DATABASE_SCHEMA \
  --liquibaseSchemaName=$LQB_DATABASE_SCHEMA \
  --changeLogFile=/changelog.yaml \
  --contexts=$LQB_CONTEXT \
  $LQB_COMMAND

# set -e

# echo "=========================================="
# echo "Starting Liquibase Migration Process"
# echo "Database: $LQB_DATABASE_URL"
# echo "Schema: $LQB_DATABASE_SCHEMA"
# echo "Command: $LQB_COMMAND"
# echo "Clear Checksums: ${LQB_CLEAR_CHECKSUMS:-false}"
# echo "=========================================="

# if [ -z "$LQB_COMMAND" ]; then
#   echo "Error: LQB_COMMAND is not set."
#   exit 1
# fi

# if [ "$LQB_CLEAR_CHECKSUMS" = "true" ]; then
#   echo ""
#   echo "=== CLEARING CHECKSUMS ==="
#   echo "Starting clearCheckSums command..."
  
#   liquibase \
#     --url=$LQB_DATABASE_URL \
#     --username=$LQB_DATABASE_USER \
#     --password=$LQB_DATABASE_PASSWORD \
#     --defaultSchemaName=$LQB_DATABASE_SCHEMA \
#     --liquibaseSchemaName=$LQB_DATABASE_SCHEMA \
#     --changeLogFile=/changelog.yaml \
#     clearCheckSums
  
#   echo "✓ Checksums cleared successfully!"
#   echo "==========================="
#   echo ""
# fi

# echo "=== RUNNING LIQUIBASE $LQB_COMMAND ==="
# echo "Starting $LQB_COMMAND command..."

# liquibase \
#   --url=$LQB_DATABASE_URL \
#   --username=$LQB_DATABASE_USER \
#   --password=$LQB_DATABASE_PASSWORD \
#   --defaultSchemaName=$LQB_DATABASE_SCHEMA \
#   --liquibaseSchemaName=$LQB_DATABASE_SCHEMA \
#   --changeLogFile=/changelog.yaml \
#   --contexts=$LQB_CONTEXT \
#   $LQB_COMMAND

# echo ""
# echo "✓ Migration completed successfully!"
# echo "=========================================="