# Set base image
ARG BASE_IMAGE=neuml/txtai-cpu:latest
FROM $BASE_IMAGE

# Start server and listen on all interfaces
ENTRYPOINT ["uvicorn", "--host", "0.0.0.0", "txtai.api:app"]
