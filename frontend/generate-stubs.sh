#!/bin/bash

set -e
src="${1}"
out="src/generated"

# clean the repo
if [[ -d "${out}" ]]
then
  rm -rf "${out}"
fi

# generate stubs
# - we do not use withSeparateModelsAndApi=true property because there is
#   a problem with one of type
./node_modules/.bin/openapi-generator-cli generate \
  -g typescript-axios \
	-i "${src}" \
	-c 'swagger-generator.conf.json' \
	-o "${out}"

# cleanup
rm -rf \
  "${out}/.openapi-generator" \
  "${out}/.gitignore" \
  "${out}/.npmignore" \
  "${out}/.openapi-generator-ignore" \
  "${out}/git_push.sh"
