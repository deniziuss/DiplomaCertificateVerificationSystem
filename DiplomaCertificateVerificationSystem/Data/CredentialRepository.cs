using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class CredentialRepository
{
    private readonly string filePath = "credentials.json";

    public void Save(List<DigitalCredential> credentials)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(credentials, options);
            File.WriteAllText(filePath, jsonString);
        }
        catch (Exception ex)
        {
            throw new Exception("Veriler kaydedilirken hata oluştu: " + ex.Message);
        }
    }

    public List<DigitalCredential> Load()
    {
        // Dosya yoksa veya içi tamamen boşsa yeni liste döndür
        if (!File.Exists(filePath))
        {
            return new List<DigitalCredential>();
        }

        try
        {
            string jsonString = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return new List<DigitalCredential>();
            }

            var credentials = JsonSerializer.Deserialize<List<DigitalCredential>>(jsonString);
            return credentials ?? new List<DigitalCredential>();
        }
        catch (Exception)
        {
            // Kendi kendine temizleme: Dosya bozulmuşsa veya uyumsuzluk varsa
            // hatayı patlatmak yerine dosyayı sıfırla ve boş liste döndür
            try
            {
                File.WriteAllText(filePath, "[]");
            }
            catch
            {
                // Dosya erişim hatası olursa yoksay
            }

            return new List<DigitalCredential>();
        }
    }
}