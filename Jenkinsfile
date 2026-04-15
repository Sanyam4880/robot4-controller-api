pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                echo 'Building the project...'
                bat 'dotnet restore'
                bat 'dotnet build'
            }
        }

        stage('Test') {
            steps {
                echo 'Running tests...'
                bat 'dotnet test'
            }
        }

        stage('Code Quality') {
            steps {
                echo 'Running code quality checks...'
                bat 'dotnet format --verify-no-changes'
            }
        }

        stage('Security') {
            steps {
                echo 'Running security scan...'
                bat 'dotnet list package --vulnerable'
            }
        }
    }

    post {
        success {
            echo 'Build, Test, Code Quality and Security stages completed successfully!'
        }
        failure {
            echo 'Pipeline failed!'
        }
    }
}