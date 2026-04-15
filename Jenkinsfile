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

        stage('Deploy') {
            steps {
                echo 'Publishing application for test deployment...'
                bat 'dotnet publish -c Release -o publish'
            }
        }
    }

    post {
        success {
            echo 'Build, Test, Code Quality, Security and Deploy stages completed successfully!'
        }
        failure {
            echo 'Pipeline failed!'
        }
    }
}